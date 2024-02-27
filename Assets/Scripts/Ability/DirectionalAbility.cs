using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ability/Directional Ability")]
public class DirectionalAbility : Ability
{
    Ray _direction;
    public Ray Direction { get { return _direction; } }






    public override bool Initialize(Vector3 srcPos, EntityData sourceEntity, Vector3? _, Ray? dir, GameObject __ = null)
    {
        if (_initialized) return false;
        if (dir == null)
        {
            Debug.LogError("Direction not assigned to Directional Ability");
            return false;
        }

        _srcPos = srcPos;
        _direction = (Ray)dir;
        _sourceEntity = sourceEntity;

        _initialized = true;
        return true;
    }
}
