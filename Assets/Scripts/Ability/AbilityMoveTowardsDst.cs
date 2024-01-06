using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability), typeof(Collider), typeof(Rigidbody))]
public class AbilityMoveTowardsDst : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] bool isHoming = false;

    private bool _hasOriented = false;

    Ability _ability;

    private void Start()
    {
        _ability = GetComponent<Ability>();
    }

    private void Awake()
    {
        _hasOriented = false;
    }

    void Update()
    {
        Orient();
        Move();
    }

    void Orient()
    {
        if (!_hasOriented || isHoming)
        {
            transform.LookAt(_ability.DstPos);
            _hasOriented = true;
        }
    }

    void Move()
    {
        transform.position = transform.position + moveSpeed * Time.deltaTime * transform.forward;
    }
}
