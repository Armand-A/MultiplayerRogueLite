using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionalAbility)), AddComponentMenu("Ability/Behaviours/Movement/Position at Source Towards Direction")]
public class AbPositionAtSrcTowardsDir : MonoBehaviour
{
    DirectionalAbility _ability;

    private void Start()
    {
        _ability = GetComponent<DirectionalAbility>();

        transform.position = _ability.SrcPos;
        transform.forward = _ability.Direction.direction;
    }
}
