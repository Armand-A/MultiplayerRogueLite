using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class ProjectileAttackBehaviour : AttackBehaviour
{
    [SerializeField] float maxDisplacement = 15f;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float radius = .5f;

    private bool _hasOriented = false;

    private void Awake()
    {
        _hasOriented = false;
        transform.localScale = 2 * radius * Vector3.one;
    }

    void Update()
    {
        Orient();
        Move();
        CheckDespawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Projectile hits enemy");
        }
    }

    void Orient()
    {
        if (!_hasOriented)
        {
            transform.LookAt(_dstPos);
            _hasOriented = true;
        }
    }

    void Move()
    {
        transform.position = transform.position + moveSpeed * Time.deltaTime * transform.forward;
    }

    void CheckDespawn()
    {
        if (Vector3.Distance(transform.position, _srcPos) > maxDisplacement)
        {
            Destroy(gameObject);
        }
    }
}
