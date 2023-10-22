using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeIndicator : AttackIndicator
{
    [SerializeField] float width = 10f;
    [SerializeField] float height = 2f;
    [SerializeField] float depth = 7f;

    private void Awake()
    {
        transform.localScale = new Vector3(width, height, depth);
    }

    private void LateUpdate()
    {
        transform.LookAt(_dstPos);
        transform.position = _srcPos + depth / 2 * transform.forward;
    }
}
