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

public enum AimMode
{
    LockOn, 
    LockOff, 
}

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float maxAimDistance = 50f;
    [SerializeField, Range(0.1f, 5f)] private float aimLockSensitivity = 2f;
    [SerializeField] private AimMode aimMode = AimMode.LockOn;
    [SerializeField] private GameObject playerAbilityManagerPrefab;
    [SerializeField] private GameObject lockOnCrosshairPrefab;

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
    }

    void Start()
    {
        _playerData = GetComponent<PlayerData>();
    }
    
    private void Update()
    {
        if (_previewingAbilitySlot != HotbarAbilitySlot.None)
        {
            UpdateLockOnTarget();
            UpdateLockOnCrosshairObject();
            UpdateAtkPos();
        }
    }

    void UpdateAtkPos()
    {
        _atkSrcPos = transform.position;
        _atkDstPos = aimMode == AimMode.LockOn ? GetAtkDstPosLockOn() : GetAtkDstPosLockOff();
        _indicator.SetPositions(_atkSrcPos, _atkDstPos);
    }

    Vector3 GetAtkDstPosLockOn()
    {
        if (lockOnTarget != null)
        {
            return lockOnTarget.transform.position;
        } 
        else
        {
            return GetAtkDstPosLockOff();
        }
    }

    Vector3 GetAtkDstPosLockOff()
    {
        Ray cameraCenterRay = CameraCenterRay;
        Vector3 dstPos = cameraCenterRay.GetPoint(maxAimDistance);

        // adjust to before hitting obstacle if hit obstacle
        RaycastHit rayHit, sphereHit;
        if (Physics.Raycast(cameraCenterRay, out rayHit, maxAimDistance, AimCastLayerMask, QueryTriggerInteraction.Ignore))
        {
            // adjust for projectile size via sphere cast for projectiles
            if (PreviewingAbility.DestinationSphereCastRadius != 0f && Physics.SphereCast(cameraCenterRay, PreviewingAbility.DestinationSphereCastRadius, out sphereHit, maxAimDistance, AimCastLayerMask, QueryTriggerInteraction.Ignore))
            {
                dstPos = cameraCenterRay.origin + Vector3.Project(sphereHit.point - cameraCenterRay.origin, rayHit.point - cameraCenterRay.origin);
            }
            else
            {
                dstPos = rayHit.point;
            }
        }

        return dstPos;
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

    private void OnDrawGizmos()
    {
        //if (lockOnTarget != null)
        //{
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawSphere(lockOnTarget.transform.position, 1f);
        //}
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

        Ability abilityObject = Instantiate(PreviewingAbility, PreviewingAbility.IsInstantiateAtDestination ? _atkDstPos : _atkSrcPos, Quaternion.identity);
        abilityObject.Initialize(_atkSrcPos, _atkDstPos, true);

        _playerAbilityManager.StartAbilityCooldown((int)_previewingAbilitySlot);

        _previewingAbilitySlot = HotbarAbilitySlot.None;

        lockOnTarget = null;
        UpdateLockOnCrosshairObject();

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

        lockOnTarget = null;
        UpdateLockOnCrosshairObject();

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
