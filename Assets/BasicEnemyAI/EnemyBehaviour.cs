using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent enemy;
    //private Transform playerLocation;
    private bool seenPlayer;
    private GameObject player;
    public bool LOS;

    // Start is called before the first frame update
    void Start()
    {
        seenPlayer = false;
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (seenPlayer)
        {
            enemy.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            //playerLocation = other.transform;
            seenPlayer = true;
            player = other.gameObject;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Attacking");
    }
}
