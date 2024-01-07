using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbPositionAtSrcTowardsDst : MonoBehaviour
{
    Ability _ability;

    private void Start()
    {
        _ability = GetComponent<Ability>();

        transform.position = _ability.SrcPos;
        transform.LookAt(_ability.DstPos);
    }
}
