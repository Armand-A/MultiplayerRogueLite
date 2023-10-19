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
    [SerializeField] float maxDistance = 15f;
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
            _attackDstPosition = transform.position + _camera.transform.forward * maxDistance;
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