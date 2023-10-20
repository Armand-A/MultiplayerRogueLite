using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDetection : MonoBehaviour
{
    [Header("Sensing Suite")]
    private NavMeshAgent enemy;
    private bool playerDetected;
    private GameObject player;
    public bool LOS;

    // Start is called before the first frame update
    void Start()
    {
        playerDetected = false;
        enemy = this.transform.parent.GetComponent<NavMeshAgent>();
        //enemy = GetComponent<NavMeshAgent>();
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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.name == "Player")
    //    {
    //        Debug.Log("Attacking");
    //    }
    //}
}
