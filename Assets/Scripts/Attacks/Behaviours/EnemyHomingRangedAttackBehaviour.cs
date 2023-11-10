using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Mirror;

public class EnemyHomingRangedAttackBehaviour : AttackBehaviour
{
    public GameObject homingProj;
    // Start is called before the first frame update
    // [Command]
    void Start()
    {
        StartCoroutine(Despawn());
        transform.LookAt(_dstPos);
        GameObject newHomingProj = Instantiate(homingProj, transform.position, transform.rotation);
        // CNetManager.OnBulletInstantiated(newHomingProj);
        // NetworkServer.Spawn(newHomingProj);
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
