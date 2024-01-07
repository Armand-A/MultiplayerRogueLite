using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbIndPositionAtDst : MonoBehaviour
{
    [SerializeField] AbIndicator abilityIndicator;

    private void LateUpdate()
    {
        transform.position = abilityIndicator.DstPos;
    }
}
