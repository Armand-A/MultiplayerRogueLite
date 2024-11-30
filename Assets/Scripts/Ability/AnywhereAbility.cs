using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ability/Anywhere Ability")]
public class AnywhereAbility : Ability
{
    [Header("Anywhere-specific")]
    [SerializeField] private AbIndicator attackIndicator;
    [SerializeField] private bool cannotCastOnEnemy;
    [SerializeField] private float destinationSphereCastRadius = 0f;

    [Header("Override indicator transforms")]
    [SerializeField] private bool useCustomIndicatorLocalPosition;
    [SerializeField] private Vector3 customIndicatorLocalPosition;
    [SerializeField] private bool useCustomIndicatorLocalRotation;
    [SerializeField] private Vector3 customIndicatorLocalRotation;
    [SerializeField] private bool useCustomIndicatorLocalScale;
    [SerializeField] private Vector3 customIndicatorLocalScale;





    public AbIndicator AttackIndicator { get { return attackIndicator; } }
    public bool IsCannotCastOnEnemy { get { return cannotCastOnEnemy; } }
    public float DestinationSphereCastRadius { get { return destinationSphereCastRadius; } }
    public bool UseCustomIndicatorLocalPosition { get { return useCustomIndicatorLocalPosition; } }
    public Vector3 CustomIndicatorLocalPosition { get { return customIndicatorLocalPosition; } }
    public bool UseCustomIndicatorLocalRotation { get { return useCustomIndicatorLocalRotation; } }
    public Vector3 CustomIndicatorLocalRotation { get { return customIndicatorLocalRotation; } }
    public bool UseCustomIndicatorLocalScale { get { return useCustomIndicatorLocalScale; } }
    public Vector3 CustomIndicatorLocalScale { get { return customIndicatorLocalScale; } }





    Vector3 _dstPos = Vector3.zero;
    public Vector3 DstPos {  get { return _dstPos; } }






    public override bool Initialize(Vector3 srcPos, EntityData sourceEntity, Vector3? dstPos, Ray? _, GameObject __ = null)
    {
        if (_initialized) return false;
        if (dstPos == null)
        {
            Debug.LogError("DstPos not assigned to Anywhere Ability");
            return false;
        }

        _srcPos = srcPos;
        _dstPos = (Vector3)dstPos;
        _sourceEntity = sourceEntity;

        _initialized = true;
        return true;
    }
}
