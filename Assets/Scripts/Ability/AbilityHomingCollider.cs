using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AbilityHomingTrigger : MonoBehaviour
{
    [SerializeField] GameObject homingTarget;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collider.enabled = false;
        IAbilityHomingable homingable = homingTarget.GetComponent<IAbilityHomingable>();
        if (homingable != null)
        {
            homingable.SetHomingTarget(other.gameObject);
        }
    }

    private void OnValidate()
    {
        if (homingTarget && homingTarget.GetComponent<IAbilityHomingable>() == null)
        {
            throw new System.Exception("Homing Target does not implement IAbilityHomingable interface.");
        }
    }
}
