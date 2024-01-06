using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbilityPositionInFrontOfSrcTowardsDst : MonoBehaviour
{
    Ability _ability;

    private void Start()
    {
        _ability = GetComponent<Ability>();
        transform.LookAt(_ability.DstPos);
        transform.position = _ability.SrcPos + transform.localScale.z / 2 * transform.forward;
    }
}
