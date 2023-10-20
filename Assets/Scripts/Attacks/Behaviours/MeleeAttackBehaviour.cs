using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehaviour : AttackBehaviour
{
    [SerializeField] float width = 10f;
    [SerializeField] float height = 2f;
    [SerializeField] float depth = 7f;
    [SerializeField] float timeBeforeDespawn = 1f;

    void Start()
    {
        StartCoroutine(Despawn());
        transform.localScale = new Vector3(width, height, depth);
        transform.LookAt(_dstPos);
        transform.position = _srcPos + depth / 2 * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Melee hits enemy");
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Destroy(gameObject);
    }
}
