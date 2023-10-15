using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float timeBeforeDespawn = 3f;
    public float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Despawn");
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Projectile hits enemy");
        }
    }

    void Move()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * moveSpeed;
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Destroy(gameObject);
    }
}
