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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Shot Player");
            Destroy(gameObject);
        }
    }

    void Move()
    {
        transform.position = transform.position + moveSpeed * Time.deltaTime * transform.forward;
    }
}
