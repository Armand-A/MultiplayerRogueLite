using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    public float timeBeforeDespawn = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Despawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
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
