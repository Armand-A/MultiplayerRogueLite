using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHomingRangedAttackBehaviour : AttackBehaviour
{
    public GameObject homingProj;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
        transform.LookAt(_dstPos);
        Instantiate(homingProj, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
