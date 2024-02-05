using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Ability/Indicator/Movement/Position At Destination")]
public class AbIndPositionAtDst : MonoBehaviour
{
    [SerializeField] AbIndicator abilityIndicator;

    private void LateUpdate()
    {
        transform.position = abilityIndicator.DstPos;
    }
}
