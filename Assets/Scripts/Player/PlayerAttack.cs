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
    [SerializeField] private ActionGauge _actionGauge;
    [SerializeField] private UnityEvent<AttackSlot> equipAttackEvent;
    [SerializeField] private UnityEvent<AttackSlot> unequipAttackEvent;

    private GameObject _camera;
    private AttackIndicator _indicator;

    private AttackSlot _equippedAttackSlot = AttackSlot.None;

    private Vector3 _attackSrcPosition;
    private Vector3 _attackDstPosition;

    private float _actionCost;

    private void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        _actionGauge = GameObject.Find("ActionGauge").GetComponent<ActionGauge>();
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

            // update attack destination location
            // get angle between camera and xz-plane, clamp to min max, and clamp to min when camera looks upward
            float cameraAngle = Vector3.Angle(_camera.transform.forward, Vector3.ProjectOnPlane(_camera.transform.forward, new Vector3(0, 1, 0)));
            cameraAngle = Mathf.Clamp(cameraAngle, cameraAngleForMinDistance, cameraAngleForMaxDistance);
            if (_camera.transform.forward.y > 0) cameraAngle = cameraAngleForMinDistance;

            // get [0,1] value from the angle
            float distanceValue = 1 - Mathf.InverseLerp(cameraAngleForMinDistance, cameraAngleForMaxDistance, cameraAngle);

            // min angle = min distance, max angle = max distance
            _attackDstPosition = transform.position + minAimDistance * _camera.transform.forward + (maxAimDistance - minAimDistance) * distanceValue * _camera.transform.forward;
            _attackDstPosition.y = transform.position.y;

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
        List<AttackScriptableObject> attacks = GetComponent<PlayerAbilities>().EquippedAbilities;

        _equippedAttackSlot = attackSlot;
        _indicator = Instantiate(attacks[(int)_equippedAttackSlot].AttackIndicator).GetComponent<AttackIndicator>();
        _actionCost = -attacks[(int)_equippedAttackSlot].ActionCost;
        _actionGauge.CostPreview(true, _actionCost);

        equipAttackEvent.Invoke(_equippedAttackSlot);
    }

    void Attack()
    {
        List<AttackScriptableObject> attacks = GetComponent<PlayerAbilities>().EquippedAbilities;

        if (!_actionGauge.UpdateActionValue(_actionCost))
            return;
        
        if (_indicator != null)
        {
            _actionGauge.CostPreview(false);
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
            _actionGauge.CostPreview(false);
            Destroy(_indicator.gameObject);
            _indicator = null;
        }
        _equippedAttackSlot = AttackSlot.None;

        unequipAttackEvent.Invoke(_equippedAttackSlot);
    }
}
