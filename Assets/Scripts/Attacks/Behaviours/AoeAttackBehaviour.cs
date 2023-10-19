using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttackBehaviour : AttackBehaviour
{
    public float timeBeforeDespawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("AreaOfEffect hits enemy");
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Destroy(gameObject);
    }
}
