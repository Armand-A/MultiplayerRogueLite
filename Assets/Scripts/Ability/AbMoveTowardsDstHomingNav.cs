using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Ability), typeof(NavMeshAgent))]
public class AbMoveTowardsDstHomingNav : MonoBehaviour, IAbilityHomingable
{
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private List<string> allowHomingTargetTags = new List<string>();

    private NavMeshAgent bullet;
    private GameObject target;
    private Ability _ability;

    void Start()
    {
        bullet = GetComponent<NavMeshAgent>();
        _ability = GetComponent<Ability>();

        transform.LookAt(_ability.DstPos);
    }

    void Update()
    {
        if (target == null)
        {
            Move();
        }
        else
        {
            bullet.SetDestination(target.transform.position);
        }
    }

    void Move()
    {
        transform.position = transform.position + moveSpeed * Time.deltaTime * transform.forward;
    }

    public void SetHomingTarget(GameObject target)
    {
        if (this.target != null) return; // remove line if allow retargeting

        foreach (string tag in allowHomingTargetTags)
        {
            if (target.CompareTag(tag))
            {
                this.target = target;
                return;
            }
        }
    }
}
