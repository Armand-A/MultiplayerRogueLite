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
    [SerializeField, Range(0.1f, 5f)] private float aimLockSensitivity = 2f;
    [SerializeField] private GameObject playerAbilityManagerPrefab;
    [SerializeField] private GameObject lockOnCrosshairPrefab;
    [SerializeField] private GameObject directionalCrosshairPrefab;

    UnityEvent<HotbarAbilitySlot> startPreviewAbilityEvent = new UnityEvent<HotbarAbilitySlot>();
    UnityEvent endPreviewAbilityEvent = new UnityEvent();

    private PlayerData _playerData;
    private PlayerAbilities _playerAbilityManager;

    private AbIndicator _indicator;

    private HotbarAbilitySlot _previewingAbilitySlot = HotbarAbilitySlot.None;

    private Vector3 _atkSrcPos;
    private Vector3 _atkDstPos;
    private GameObject lockOnTarget = null;
    private GameObject lockOnCrosshairObject;
    private Ray direction = new Ray();
    private GameObject directionalCrosshairObject;

    private float _actionCost;
    private PlayerCamera _playerCamera;

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

    private bool IsPreviewingAbility
    {
        get
        {
            return _previewingAbilitySlot != HotbarAbilitySlot.None;
        }
    }

    Ray CameraCenterRay { get { return Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); } }
    LayerMask AimCastLayerMask 
    { 
        get
        {
            LayerMask mask = 0;
            mask |= 1 << LayerMask.NameToLayer("Default");
            mask |= 1 << LayerMask.NameToLayer("Ground");
            mask |= 1 << LayerMask.NameToLayer("Water");
            mask |= 1 << LayerMask.NameToLayer("Block Attack");
            return mask;
        } 
    }

    private void Awake()
    {
        _playerAbilityManager = FindObjectOfType<PlayerAbilities>();
        if (_playerAbilityManager == null)
        {
            _playerAbilityManager = Instantiate(playerAbilityManagerPrefab).GetComponent<PlayerAbilities>();
        }

        lockOnCrosshairObject = Instantiate(lockOnCrosshairPrefab);
        lockOnCrosshairObject.SetActive(false);
        directionalCrosshairObject = Instantiate(directionalCrosshairPrefab);
        directionalCrosshairObject.SetActive(false);
    }

    void Start()
    {
        _playerData = GetComponent<PlayerData>();
        _playerCamera = FindObjectOfType<PlayerCamera>();
    }
    
    private void Update()
    {
        if (IsPreviewingAbility)
        {
            _atkSrcPos = transform.position;

            if (PreviewingAbility is TargetedAbility)
            {
                UpdateLockOnTarget();
                UpdateLockOnCrosshairObject();
            }

            if (PreviewingAbility is AnywhereAbility)
            {
                UpdateDstPos();
                UpdateIndicatorObject();
            }

            if (PreviewingAbility is DirectionalAbility)
            {
                UpdateDirection();
                UpdateDirectionalCrosshairObject();
            }

        }
    }

    void UpdateIndicatorObject()
    {
        if (_indicator != null)
        {
            _indicator.SetPositions(_atkSrcPos, _atkDstPos);
        }
    }

    void UpdateDirection()
    {
        direction.origin = transform.position;
        direction.direction = Camera.main.transform.forward;
    }

    void UpdateDirectionalCrosshairObject()
    {
        if (IsPreviewingAbility)
        {
            if (!directionalCrosshairObject.activeInHierarchy)
            {
                directionalCrosshairObject.SetActive(true);
            }
            directionalCrosshairObject.transform.position = direction.origin;
            directionalCrosshairObject.transform.forward = direction.direction;
        }
        else
        {
            if (directionalCrosshairObject.activeInHierarchy)
            {
                directionalCrosshairObject.SetActive(false);
            }
        }

    }

    void UpdateDstPos()
    {
        if (PreviewingAbility is AnywhereAbility anywhereAbility)
        {
            Ray cameraCenterRay = CameraCenterRay;
            _atkDstPos = cameraCenterRay.GetPoint(maxAimDistance);

            // adjust to before hitting obstacle if hit obstacle
            RaycastHit rayHit, sphereHit;
            if (Physics.Raycast(cameraCenterRay, out rayHit, maxAimDistance, AimCastLayerMask, QueryTriggerInteraction.Ignore))
            {
                // adjust for projectile size via sphere cast for projectiles
                if (anywhereAbility.DestinationSphereCastRadius != 0f && Physics.SphereCast(cameraCenterRay, anywhereAbility.DestinationSphereCastRadius, out sphereHit, maxAimDistance, AimCastLayerMask, QueryTriggerInteraction.Ignore))
                {
                    _atkDstPos = cameraCenterRay.origin + Vector3.Project(sphereHit.point - cameraCenterRay.origin, rayHit.point - cameraCenterRay.origin);
                }
                else
                {
                    _atkDstPos = rayHit.point;
                }
            }
        }
    }

    void UpdateLockOnTarget()
    {
        Ray cameraCenterRay = CameraCenterRay;
        RaycastHit[] sphereHits = Physics.SphereCastAll(cameraCenterRay, aimLockSensitivity, maxAimDistance, AimCastLayerMask, QueryTriggerInteraction.Ignore);

        GameObject closestObj = null;
        float closestObjDot = float.MinValue; // dist from aim center
        foreach (RaycastHit hit in sphereHits)
        {
            // if hit object is not enemy, skip object
            EnemyData enemyData = hit.collider.gameObject.GetComponentInParent<EnemyData>();
            if (enemyData == null) continue;

            float thisDist = Vector3.Dot(cameraCenterRay.direction.normalized, (hit.point - cameraCenterRay.origin).normalized);

            // if this object further from center of screen than cached object, skip object
            if (thisDist <= closestObjDot) continue;

            // cache current object
            closestObj = enemyData.gameObject;
            closestObjDot = thisDist;
        }
        lockOnTarget = closestObj;
    }

    void UpdateLockOnCrosshairObject()
    {
        if (lockOnTarget != null)
        {
            lockOnCrosshairObject.transform.position = lockOnTarget.transform.position;
            if (!lockOnCrosshairObject.activeInHierarchy)
            {
                lockOnCrosshairObject.SetActive(true);
            }
        }
        else if (lockOnCrosshairObject.activeInHierarchy)
        {
            lockOnCrosshairObject.SetActive(false);
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
        if (PreviewingAbility is AnywhereAbility anywhereAbility)
        {
            _indicator = Instantiate(PreviewingAbility.AttackIndicator).GetComponent<AbIndicator>();
            _indicator.Initialize(anywhereAbility);
        } else if (PreviewingAbility is DirectionalAbility)
        {
            _playerCamera.SwitchCameraStyle(PlayerCamera.CameraStyle.Aim);
        }

        //Allows preview cost of equipped action
        _actionCost = PreviewingAbility.ActionCost;
        _playerData.PreviewActionCost(true, _actionCost);

        startPreviewAbilityEvent.Invoke(hotbarAbilitySlot);
    }

    void Attack()
    {
        // check if attack cannot be cast on enemy
        if (_indicator != null && PreviewingAbility is AnywhereAbility anywhereAbility && anywhereAbility.IsCannotCastOnEnemy && _indicator.HasEnemyInRange) return;

        // check if ability is on cooldown, should've been checked by equip but just to be sure
        if (!_playerAbilityManager.GetIsAbilityAvailable((int)_previewingAbilitySlot)) return;

        // check if there is a target if ability's aim mode needs a target i.e. aimMode == Targeted
        if (PreviewingAbility is TargetedAbility && lockOnTarget == null) return;

        // Checks and consumes action points depending on if there is enough left
        if (!_playerData.UseAction(_actionCost))
            return;

        Ability abilityObject = Instantiate(PreviewingAbility, PreviewingAbility is AnywhereAbility ? _atkDstPos : _atkSrcPos, Quaternion.identity);
        abilityObject.Initialize(_atkSrcPos, _playerData, _atkDstPos, new Ray(transform.position, Camera.main.transform.forward), lockOnTarget);

        _playerAbilityManager.StartAbilityCooldown((int)_previewingAbilitySlot);

        ResetToIdle();
    }

    void CancelAttack()
    {
        ResetToIdle();
    }

    void ResetToIdle()
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
        _playerCamera.SwitchCameraStyle(PlayerCamera.CameraStyle.Basic);

        lockOnTarget = null;
        UpdateLockOnCrosshairObject();
        UpdateDirectionalCrosshairObject();

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
