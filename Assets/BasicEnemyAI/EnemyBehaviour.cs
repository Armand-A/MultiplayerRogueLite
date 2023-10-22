using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public bool playerDetected;
    public GameObject player;
    private bool onCooldown;
    public NavMeshAgent enemy;
    public bool ranged;
    public GameObject meleeAttack;
    public GameObject rangedAttack;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        onCooldown = false;
        if(ranged)
        {
            gameObject.GetComponent<SphereCollider>().radius = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDetected)
        {
            enemy.SetDestination(player.transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.speed = 0;
            if (!onCooldown)
            {
                onCooldown = true;
                if(ranged)
                {
                    RangedAttack();
                }
                else if(!ranged)
                {
                    MeleeAttack();
                }
                StartCoroutine(Cooldown());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.speed = 3.5f;
        }
    }

    void MeleeAttack()
    {
        Instantiate(meleeAttack, gameObject.transform.position, gameObject.transform.rotation);
    }

    void RangedAttack()
    {
        Instantiate(rangedAttack, player.transform.position, player.transform.rotation);
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        onCooldown = false;
    }
}
