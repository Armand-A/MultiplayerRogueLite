using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackBehaviour : AttackBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
        transform.LookAt(_dstPos);
        transform.position = _srcPos + 4 / 2 * transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Smacked Player");
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
