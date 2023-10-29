using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyRangedAttackBehaviour : AttackBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(_dstPos);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Shot Player");
        }
    }

    void Move()
    {
        transform.position = transform.position + moveSpeed * Time.deltaTime * transform.forward;
    }
}
