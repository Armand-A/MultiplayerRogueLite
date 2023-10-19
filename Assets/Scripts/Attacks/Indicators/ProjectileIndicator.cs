using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileIndicator : AttackIndicator
{
    private void Update()
    {
        transform.position = _srcPos;
        transform.LookAt(_dstPos);
    }
}
