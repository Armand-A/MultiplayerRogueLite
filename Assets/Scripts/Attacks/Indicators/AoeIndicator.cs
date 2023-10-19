using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Renderer))]
public class AoeIndicator : AttackIndicator
{
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material normalMaterial;
    [SerializeField] float radius = 4.5f;
    [SerializeField] float height = 1f;
    private int _enemyCountInCollider;

    private void Awake()
    {
        transform.localScale = new Vector3(radius * 2, height, radius * 2);
    }

    private void LateUpdate()
    {
        transform.position = _dstPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemyCountInCollider++;
            if (highlightMaterial != null && normalMaterial != null) GetComponent<Renderer>().material = highlightMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemyCountInCollider--;
            if (_enemyCountInCollider == 0 && highlightMaterial != null && normalMaterial != null) GetComponent<Renderer>().material = normalMaterial;
        }
    }
}
