using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnywhereAbility)), AddComponentMenu("Ability/Behaviours/Movement/Move towards Destination")]
public class AbMoveTowardsDst : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] bool isHoming = false;

    bool _hasOriented = false;
    AnywhereAbility _ability;

    private void Start()
    {
        _ability = GetComponent<AnywhereAbility>();
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
