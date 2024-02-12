using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AttackSlot 
{ 
    Slot1, 
    Slot2, 
    Slot3, 
    Slot4, 
    None, 
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] float minAimDistance = 5f;
    [SerializeField] float maxAimDistance = 50f;
    
    [SerializeField] private List<AttackScriptableObject> attacks;
    [SerializeField] private GameObject playerAbilityManagerPrefab;

    UnityEvent<AttackSlot> equipAttackEvent = new UnityEvent<AttackSlot>();
    UnityEvent unequipAttackEvent = new UnityEvent();

    private PlayerData _playerData;
    private PlayerAbilities _playerAbilityManager;

    private GameObject _camera;
    private AttackIndicator _indicator;

    private AttackSlot _equippedAttackSlot = AttackSlot.None;
    public AttackSlot EquipedAttackSlot { get { return _equippedAttackSlot; } }

    private Vector3 _attackSrcPosition;
    private Vector3 _attackDstPosition;

    private float _actionCost;

    private void Awake()
    {
        _playerAbilityManager = FindObjectOfType<PlayerAbilities>();
        if (_playerAbilityManager == null)
        {
            _playerAbilityManager = Instantiate(playerAbilityManagerPrefab).GetComponent<PlayerAbilities>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerData = GetComponent<PlayerData>();
        _camera = GameObject.FindWithTag("MainCamera");

        //if (equipAttackEvent != null) equipAttackEvent = new UnityEvent<AttackSlot>();
        //if (unequipAttackEvent != null) unequipAttackEvent = new UnityEvent<AttackSlot>();
    }

    private void Update()
    {
        if (_equippedAttackSlot != AttackSlot.None)
        {
            // update attack source location
            _attackSrcPosition = transform.position;

            // get y value from camera, 1 is most downward, 0 is most upward
            CinemachineFreeLook activeVirtualCamera = _camera.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineFreeLook>();
            // invert it, 0 is most downward, 1 is most upward
            float distanceValue = 1 - activeVirtualCamera.m_YAxis.Value;

            // get camera's forward angle projected on xz plane
            Vector3 cameraForwardFlatAngle = _camera.transform.forward;
            cameraForwardFlatAngle.y = 0;
            cameraForwardFlatAngle.Normalize();

            // min angle = min distance, max angle = max distance
            _attackDstPosition = transform.position + minAimDistance * cameraForwardFlatAngle + (maxAimDistance - minAimDistance) * distanceValue * cameraForwardFlatAngle;

            // update attack destination location
            _indicator.SetPositions(_attackSrcPosition, _attackDstPosition);
        }
    }

    public void OnEquipAttack(AttackSlot attackSlot)
    {
        if (_equippedAttackSlot != attackSlot)
        {
            CancelAttack();
            Equip(attackSlot);
        }
        else
        {
            CancelAttack();
        }
    }

    public void OnAttack()
    {
        if (_equippedAttackSlot == AttackSlot.None)
        {
            // TODO: basic attack?
        }
        else Attack();
    }

    public void OnCancelAttack()
    {
        if (_equippedAttackSlot == AttackSlot.None) return;

        CancelAttack();
    }

    void Equip(AttackSlot attackSlot)
    {
        List<AttackScriptableObject> attacks = _playerAbilityManager.EquippedAbilities;

        // check if there is ability equipped on selected slot
        if (attacks[(int)attackSlot] == null) return;

        // check if ability is on cooldown
        if (!_playerAbilityManager.GetIsAbilityAvailable((int)attackSlot)) return;

        _equippedAttackSlot = attackSlot;
        _indicator = Instantiate(attacks[(int)_equippedAttackSlot].AttackIndicator).GetComponent<AttackIndicator>();

        //Allows preview cost of equipped action
        _actionCost = attacks[(int)_equippedAttackSlot].ActionCost;
        _playerData.PreviewActionCost(true, _actionCost);

        equipAttackEvent.Invoke(attackSlot);
    }

    void Attack()
    {
        List<AttackScriptableObject> attacks = _playerAbilityManager.EquippedAbilities;
        
        // check if attack cannot be cast on enemy
        if (_indicator != null && attacks[(int)_equippedAttackSlot].IsCannotCastOnEnemy && _indicator.HasEnemyInRange) return;
        
        // check if ability is on cooldown, should've been checked by equip but just to be sure
        if (!_playerAbilityManager.GetIsAbilityAvailable((int)_equippedAttackSlot)) return;
        
        // Checks and consumes action points depending on if there is enough left
        if (!_playerData.UpdateAction(_actionCost)) return;
        

        if (_indicator != null)
        {

            //Removes action cost preview
            _playerData.PreviewActionCost(false);

            Destroy(_indicator.gameObject);
            _indicator = null;
        }

        AttackBehaviour attackObject = Instantiate(attacks[(int)_equippedAttackSlot].AttackBehaviour, attacks[(int)_equippedAttackSlot].AttackBehaviour.GetIsInstantiateAtDestination() ? _attackDstPosition : _attackSrcPosition, Quaternion.identity);
        attackObject.SetPositions(_attackSrcPosition, _attackDstPosition);
        attackObject.SetDamage(attacks[(int)_equippedAttackSlot].Damage);
        attackObject.SetIsFromPlayer(true);

        _playerAbilityManager.StartAbilityCooldown((int)_equippedAttackSlot);

        _equippedAttackSlot = AttackSlot.None;

        unequipAttackEvent.Invoke();
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
        _equippedAttackSlot = AttackSlot.None;

        unequipAttackEvent.Invoke();
    }

    public void AddEquipListener(UnityAction<AttackSlot> action)
    {
        equipAttackEvent.AddListener(action);
    }

    public void RemoveEquipListener(UnityAction<AttackSlot> action)
    {
        equipAttackEvent.RemoveListener(action);
    }

    public void AddUnequipListener(UnityAction action)
    {
        unequipAttackEvent.AddListener(action);
    }

    public void RemoveUnequipListener(UnityAction action)
    {
        unequipAttackEvent.RemoveListener(action);
    }
}
