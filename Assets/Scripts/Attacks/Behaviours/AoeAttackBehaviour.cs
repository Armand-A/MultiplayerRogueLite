using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttackBehaviour : AttackBehaviour
{
    [SerializeField] float radius = 4.5f;
    [SerializeField] float height = 1f;
    [SerializeField] float timeBeforeDespawn = 1f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
        transform.localScale = new Vector3(radius * 2, height, radius * 2);
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
