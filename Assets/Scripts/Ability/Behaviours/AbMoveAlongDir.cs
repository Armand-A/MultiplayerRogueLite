using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DirectionalAbility)), AddComponentMenu("Ability/Behaviours/Movement/Move Along Direction")]
public class AbMoveAlongDir : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] bool isHoming = false;

    bool _hasOriented = false;
    DirectionalAbility _ability;

    private void Start()
    {
        _ability = GetComponent<DirectionalAbility>();
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
            _ability.transform.forward = _ability.Direction.direction;
            _hasOriented = true;
        }
    }

    void Move()
    {
        _ability.transform.position = _ability.transform.position + moveSpeed * Time.deltaTime * _ability.transform.forward;
    }
}
