using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ability/Indicator/Ability Indicator")]
public class AbIndicator : MonoBehaviour
{
    [SerializeField, Tooltip("Applies the transform from the source in Ability to this transform destination, if both are specified.")] 
    Transform transformDestination;

    Vector3 _srcPos = Vector3.zero;
    Vector3 _dstPos = Vector3.zero;
    bool _hasEnemyInRange;
    Ability _parentAbility;

    public Vector3 SrcPos { get { return _srcPos; } }
    public Vector3 DstPos { get { return _dstPos; } }
    public bool HasEnemyInRange { get { return _hasEnemyInRange; } }

    public void Initialize(Ability parentAbility)
    {
        _parentAbility = parentAbility;
        if (transformDestination != null)
        {
            if (_parentAbility.UseCustomIndicatorLocalPosition)
            {
                transformDestination.transform.localPosition = _parentAbility.CustomIndicatorLocalPosition;
            }
            if (_parentAbility.UseCustomIndicatorLocalRotation)
            {
                transformDestination.transform.localRotation = Quaternion.Euler(_parentAbility.CustomIndicatorLocalRotation);
            }
            if (_parentAbility.UseCustomIndicatorLocalScale)
            {
                transformDestination.transform.localScale = _parentAbility.CustomIndicatorLocalScale;
            }
        }
    }

    public void SetPositions(Vector3 srcPos, Vector3 dstPos)
    {
        _srcPos = srcPos;
        _dstPos = dstPos;
    }

    public void OnEnemyCounterChange(int counter)
    {
        if (counter > 0)
        {
            _hasEnemyInRange = true;
        }
        else
        {
            _hasEnemyInRange = false;
        }
    }
}
