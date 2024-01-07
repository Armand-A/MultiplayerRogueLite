using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IAbilityHomingable
{
    void SetHomingTarget(GameObject target);
}

[RequireComponent(typeof(Collider))]
public class HomingTrigger : MonoBehaviour
{
    [SerializeField] GameObject objectToReceiveHoming;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        IAbilityHomingable homingable = objectToReceiveHoming.GetComponent<IAbilityHomingable>();
        if (homingable != null)
        {
            homingable.SetHomingTarget(other.gameObject);
        }
    }

    private void OnValidate()
    {
        if (objectToReceiveHoming && objectToReceiveHoming.GetComponent<IAbilityHomingable>() == null)
        {
            throw new System.Exception(objectToReceiveHoming.name + ": Object to receive homing does not have a component that implements IAbilityHomingable interface.");
        }
    }
}
