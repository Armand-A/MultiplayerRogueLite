using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ability/Targeted Ability")]
public class TargetedAbility : Ability
{
    GameObject _targetObject;
    public GameObject TargetObject {  get { return _targetObject; } }

    public override bool Initialize(Vector3 srcPos, EntityData sourceEntity, Vector3? _, Ray? __, GameObject targetObject = null)
    {
        if (_initialized) return false;
        if (targetObject == null)
        {
            Debug.LogError("Target not assigned to Targeted Ability");
            return false;
        }

        _srcPos = srcPos;
        _sourceEntity = sourceEntity;
        _targetObject = targetObject;

        _initialized = true;
        return true;
    }
}
