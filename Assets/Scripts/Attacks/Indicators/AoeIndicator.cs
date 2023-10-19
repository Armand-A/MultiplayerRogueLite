using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeIndicator : AttackIndicator
{
    private void Update()
    {
        transform.position = _dstPos;
    }
}
