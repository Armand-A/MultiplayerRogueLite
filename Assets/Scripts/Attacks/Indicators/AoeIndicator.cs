using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Renderer))]
public class AoeIndicator : AttackIndicator
{
    [SerializeField] float radius = 4.5f;
    [SerializeField] float height = 1f;

    private void Awake()
    {
        transform.localScale = new Vector3(radius * 2, height, radius * 2);
    }

    private void LateUpdate()
    {
        transform.position = _dstPos;
    }
}
