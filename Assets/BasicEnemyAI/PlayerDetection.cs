using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDetection : MonoBehaviour
{
    [Header("Sensing Suite")]
    private NavMeshAgent enemy;
    //private Transform playerLocation;
    private bool playerDetected;
    private GameObject player;
    public bool LOS;

    // Start is called before the first frame update
    void Start()
    {
        playerDetected = false;
        enemy = GetComponent<NavMeshAgent>();
        LOS = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            enemy.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            //playerLocation = other.transform;
            playerDetected = true;
            player = other.gameObject;
            Debug.Log("I see the player");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Attacking");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = (LOS) ? Color.green : Color.red;

        if (playerDetected)
        {
            //Gizmos.DrawLine(transform.position, playerTransform.position);
        }
        Gizmos.color = (playerDetected) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, 5.0f);
    }
}
