using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbIndicator : MonoBehaviour
{
    Vector3 _srcPos = Vector3.zero;
    Vector3 _dstPos = Vector3.zero;
    bool _hasEnemyInRange;

    public Vector3 SrcPos { get { return _srcPos; } }
    public Vector3 DstPos { get { return _dstPos; } }
    public bool HasEnemyInRange { get { return _hasEnemyInRange; } }

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
