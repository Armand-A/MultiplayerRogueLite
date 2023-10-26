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
    [SerializeField] float minAimDistance = 10f;
    [SerializeField] float maxAimDistance = 30f;
    [SerializeField] float cameraAngleForMinDistance = 10f;
    [SerializeField] float cameraAngleForMaxDistance = 30f;
    [SerializeField] private List<AttackScriptableObject> attacks;
    [SerializeField] private UnityEvent<AttackSlot> equipAttackEvent;
    [SerializeField] private UnityEvent<AttackSlot> unequipAttackEvent;

    private PlayerData _playerData;

    private GameObject _camera;
    private AttackIndicator _indicator;

    private AttackSlot _equippedAttackSlot = AttackSlot.None;

    private Vector3 _attackSrcPosition;
    private Vector3 _attackDstPosition;

    private float _actionCost;

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
        _equippedAttackSlot = attackSlot;
        _indicator = Instantiate(attacks[(int)_equippedAttackSlot].AttackIndicator).GetComponent<AttackIndicator>();
        _actionCost = -attacks[(int)_equippedAttackSlot].ActionCost;
        _playerData.PreviewActionCost(true, _actionCost);

        equipAttackEvent.Invoke(_equippedAttackSlot);
    }

    void Attack()
    {
        if (!_playerData.PlayerAction.UpdateValue(_actionCost))
            return;
        
        if (_indicator != null)
        {
            _playerData.PreviewActionCost(false);
            Destroy(_indicator.gameObject);
            _indicator = null;
        }

        AttackBehaviour attackObject = Instantiate(attacks[(int)_equippedAttackSlot].AttackBehaviour, attacks[(int)_equippedAttackSlot].AttackBehaviour.GetIsInstantiateAtDestination() ? _attackDstPosition : _attackSrcPosition, Quaternion.identity);
        attackObject.SetPositions(_attackSrcPosition, _attackDstPosition);

        _equippedAttackSlot = AttackSlot.None;

        unequipAttackEvent.Invoke(_equippedAttackSlot);
    }

    void CancelAttack()
    {
        _actionCost = 0;
        if (_indicator != null)
        {
            _playerData.PreviewActionCost(false);
            Destroy(_indicator.gameObject);
            _indicator = null;
        }
        _equippedAttackSlot = AttackSlot.None;

        unequipAttackEvent.Invoke(_equippedAttackSlot);
    }
}
