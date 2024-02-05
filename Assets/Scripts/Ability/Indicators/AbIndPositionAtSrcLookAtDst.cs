using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ability/Indicator/Movement/Position at Source Towards Destination")]
public class AbIndPositionAtSrcLookAtDst : MonoBehaviour
{
    [SerializeField] AbIndicator abilityIndicator;

    private void LateUpdate()
    {
        transform.position = abilityIndicator.SrcPos;
        transform.LookAt(abilityIndicator.DstPos);
    }
}
