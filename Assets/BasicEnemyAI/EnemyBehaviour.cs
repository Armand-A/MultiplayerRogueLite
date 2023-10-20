using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private bool onCooldown;
    // Start is called before the first frame update
    void Start()
    {
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (!onCooldown)
            {
                onCooldown = true;
                Debug.Log("Attacking");
                StartCoroutine(Cooldown());
            }
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        onCooldown = false;
    }
}
