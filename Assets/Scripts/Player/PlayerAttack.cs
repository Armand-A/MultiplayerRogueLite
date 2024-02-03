using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public enum HotbarAbilitySlot 
{ 
    Slot1, 
    Slot2, 
    Slot3, 
    Slot4, 
    None, 
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float maxAimDistance = 50f;

    [SerializeField] private GameObject playerAbilityManagerPrefab;

    UnityEvent<HotbarAbilitySlot> startPreviewAbilityEvent = new UnityEvent<HotbarAbilitySlot>();
    UnityEvent endPreviewAbilityEvent = new UnityEvent();

    private PlayerData _playerData;
    private PlayerAbilities _playerAbilityManager;

    private GameObject _camera;
    private AbIndicator _indicator;

    private HotbarAbilitySlot _previewingAbilitySlot = HotbarAbilitySlot.None;

    private Vector3 _attackSrcPosition;
    private Vector3 _attackDstPosition;

    private float _actionCost;

    private List<Ability> EquippedAbilities
    {
        get
        {
            return _playerAbilityManager.EquippedAbilities;
        }
    }

    private Ability PreviewingAbility
    {
        get
        {
            return _playerAbilityManager.EquippedAbilities[(int)_previewingAbilitySlot];
        }
    }

    private void Awake()
    {
        _playerAbilityManager = FindObjectOfType<PlayerAbilities>();
        if (_playerAbilityManager == null)
        {
            _playerAbilityManager = Instantiate(playerAbilityManagerPrefab).GetComponent<PlayerAbilities>();
        }
    }

    void Start()
    {
        _playerData = GetComponent<PlayerData>();
        _camera = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if (_previewingAbilitySlot != HotbarAbilitySlot.None)
        {
            _attackSrcPosition = transform.position;
            _attackDstPosition = _camera.transform.position + _camera.transform.forward.normalized * maxAimDistance;

            LayerMask raycastLayermask = 0;
            raycastLayermask |= 1 << LayerMask.NameToLayer("Default");
            raycastLayermask |= 1 << LayerMask.NameToLayer("Ground");
            raycastLayermask |= 1 << LayerMask.NameToLayer("Water");
            raycastLayermask |= 1 << LayerMask.NameToLayer("Block Attack");

            // adjust if hit obstacle
            RaycastHit rayHit, sphereHit;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out rayHit, maxAimDistance, raycastLayermask, QueryTriggerInteraction.Ignore))
            {
                // adjust for projectile size via sphere cast for projectiles
                if (PreviewingAbility.DestinationSphereCastRadius != 0f && Physics.SphereCast(_camera.transform.position, PreviewingAbility.DestinationSphereCastRadius, _camera.transform.forward, out sphereHit, maxAimDistance, raycastLayermask, QueryTriggerInteraction.Ignore))
                {
                    _attackDstPosition = _camera.transform.position + Vector3.Project(sphereHit.point - _camera.transform.position, rayHit.point - _camera.transform.position);
                }
                else
                {
                    _attackDstPosition = rayHit.point;
                }
            }

            _indicator.SetPositions(_attackSrcPosition, _attackDstPosition);
        }
    }

    public void OnHotbarAbilityPressed(HotbarAbilitySlot hotbarAbilitySlot)
    {
        if (_previewingAbilitySlot != hotbarAbilitySlot)
        {
            CancelAttack();
            PreviewAttack(hotbarAbilitySlot);
        }
        else
        {
            CancelAttack();
        }
    }

    public void OnAttack()
    {
        if (_previewingAbilitySlot == HotbarAbilitySlot.None)
        {
            // TODO: basic attack?
        }
        else Attack();
    }

    public void OnCancelAttack()
    {
        if (_previewingAbilitySlot == HotbarAbilitySlot.None) return;

        CancelAttack();
    }

    void PreviewAttack(HotbarAbilitySlot hotbarAbilitySlot)
    {
        // check if there is ability equipped on selected slot
        if (EquippedAbilities[(int)hotbarAbilitySlot] == null) return;

        // check if ability is on cooldown
        if (!_playerAbilityManager.GetIsAbilityAvailable((int)hotbarAbilitySlot)) return;

        _previewingAbilitySlot = hotbarAbilitySlot;
        _indicator = Instantiate(PreviewingAbility.AttackIndicator).GetComponent<AbIndicator>();
        _indicator.Initialize(PreviewingAbility);

        //Allows preview cost of equipped action
        _actionCost = -PreviewingAbility.ActionCost;
        _playerData.PreviewActionCost(true, _actionCost);

        startPreviewAbilityEvent.Invoke(hotbarAbilitySlot);
    }

    void Attack()
    {
        // check if attack cannot be cast on enemy
        if (_indicator != null && PreviewingAbility.IsCannotCastOnEnemy && _indicator.HasEnemyInRange) return;

        // check if ability is on cooldown, should've been checked by equip but just to be sure
        if (!_playerAbilityManager.GetIsAbilityAvailable((int)_previewingAbilitySlot)) return;

        // Checks and consumes action points depending on if there is enough left
        if (!_playerData.UpdateAction(_actionCost))
            return;
        
        if (_indicator != null)
        {

            //Removes action cost preview
            _playerData.PreviewActionCost(false);

            Destroy(_indicator.gameObject);
            _indicator = null;
        }

        Ability abilityObject = Instantiate(PreviewingAbility, PreviewingAbility.IsInstantiateAtDestination ? _attackDstPosition : _attackSrcPosition, Quaternion.identity);
        abilityObject.Initialize(_attackSrcPosition, _attackDstPosition, true);

        _playerAbilityManager.StartAbilityCooldown((int)_previewingAbilitySlot);

        _previewingAbilitySlot = HotbarAbilitySlot.None;

        endPreviewAbilityEvent.Invoke();
    }

    void CancelAttack()
    {
        _actionCost = 0;
        if (_indicator != null)
        {
            //Removes action cost preview
            _playerData.PreviewActionCost(false);

            Destroy(_indicator.gameObject);
            _indicator = null;
        }
        _previewingAbilitySlot = HotbarAbilitySlot.None;

        endPreviewAbilityEvent.Invoke();
    }

    public void AddStartPreviewAbilityListener(UnityAction<HotbarAbilitySlot> action)
    {
        startPreviewAbilityEvent.AddListener(action);
    }

    public void RemoveStartPreviewAbilityListener(UnityAction<HotbarAbilitySlot> action)
    {
        startPreviewAbilityEvent.RemoveListener(action);
    }

    public void AddEndPreviewAbilityListener(UnityAction action)
    {
        endPreviewAbilityEvent.AddListener(action);
    }

    public void RemoveEndPreviewAbilityListener(UnityAction action)
    {
        endPreviewAbilityEvent.RemoveListener(action);
    }
}
