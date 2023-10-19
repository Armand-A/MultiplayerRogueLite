using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject _camera;
    private AttackIndicator _indicator;

    private AttackSlot _equippedAttackSlot = AttackSlot.None;

    private Vector3 _attackSrcPosition;
    private Vector3 _attackDstPosition;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindWithTag("MainCamera");
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
        if (_equippedAttackSlot != attackSlot) Equip(attackSlot);
        else Attack();
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
    }

    void Attack()
    {
        if (_indicator != null)
        {
            Destroy(_indicator.gameObject);
            _indicator = null;
        }

        AttackBehaviour attackObject = Instantiate(attacks[(int)_equippedAttackSlot].AttackBehaviour, attacks[(int)_equippedAttackSlot].AttackBehaviour.GetIsInstantiateAtDestination() ? _attackDstPosition : _attackSrcPosition, Quaternion.identity);
        attackObject.SetPositions(_attackSrcPosition, _attackDstPosition);

        _equippedAttackSlot = AttackSlot.None;
    }

    void CancelAttack()
    {
        if (_indicator != null)
        {
            Destroy(_indicator.gameObject);
            _indicator = null;
        }
        _equippedAttackSlot = AttackSlot.None;
    }
}