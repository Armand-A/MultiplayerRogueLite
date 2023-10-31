using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAttackBehaviour : AttackBehaviour
{
    [SerializeField] Vector3 size = Vector3.one;
    [SerializeField] float timeBeforeDespawn = 1f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
        transform.localScale = size;
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Destroy(gameObject);
    }
}
