using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileIndicator : AttackIndicator
{
    [SerializeField] float length = 10f;
    [SerializeField] float radius = .5f;

    private void Awake()
    {
        transform.localScale = new Vector3(radius * 2, radius * 2, length);
    }

    private void LateUpdate()
    {
        transform.position = _srcPos;
        transform.LookAt(_dstPos);
    }
}
