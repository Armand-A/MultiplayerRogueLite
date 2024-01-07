using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbIndPositionAtSrcLookAtDst : MonoBehaviour
{
    [SerializeField] AbIndicator abilityIndicator;

    private void LateUpdate()
    {
        transform.position = abilityIndicator.SrcPos;
        transform.LookAt(abilityIndicator.DstPos);
    }
}
