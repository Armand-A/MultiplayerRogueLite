using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability)), AddComponentMenu("Ability/Movement/Move towards Destination")]
public class AbMoveTowardsDst : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] bool isHoming = false;

    bool _hasOriented = false;
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
            _ability.transform.LookAt(_ability.DstPos);
            _hasOriented = true;
        }
    }

    void Move()
    {
        _ability.transform.position = _ability.transform.position + moveSpeed * Time.deltaTime * _ability.transform.forward;
    }
}
