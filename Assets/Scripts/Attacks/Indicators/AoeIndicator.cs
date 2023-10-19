using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeIndicator : AttackIndicator
{
    private void LateUpdate()
    {
        transform.position = _dstPos;
    }
}
