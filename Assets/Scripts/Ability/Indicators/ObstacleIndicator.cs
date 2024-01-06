using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Renderer))]
public class ObstacleIndicator : AttackIndicator
{
    [SerializeField] Vector3 size = Vector3.one;

    private void Awake()
    {
        transform.localScale = size;
    }

    private void LateUpdate()
    {
        transform.position = _dstPos;
    }
}
