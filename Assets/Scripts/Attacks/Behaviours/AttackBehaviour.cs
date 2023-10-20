using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private bool isInstantiateAtDestination;

    protected Vector3 _srcPos = Vector3.zero;
    protected Vector3 _dstPos = Vector3.zero;

    public void SetPositions(Vector3 srcPos, Vector3 dstPos)
    {
        _srcPos = srcPos;
        _dstPos = dstPos;
    }

    public bool GetIsInstantiateAtDestination()
    {
        return isInstantiateAtDestination; 
    }
}
