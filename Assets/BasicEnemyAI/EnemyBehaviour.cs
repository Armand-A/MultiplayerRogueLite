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
    [SerializeField] private AttackScriptableObject meleeAttack;
    [SerializeField] private AttackScriptableObject rangedAttack;
    public int Health;

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
        AttackBehaviour attackObject = Instantiate(meleeAttack.AttackBehaviour, meleeAttack.AttackBehaviour.GetIsInstantiateAtDestination() ? player.transform.position : gameObject.transform.position, Quaternion.identity);
        attackObject.SetPositions(gameObject.transform.position, player.transform.position);
    }

    void RangedAttack()
    {
        AttackBehaviour attackObject = Instantiate(rangedAttack.AttackBehaviour, rangedAttack.AttackBehaviour.GetIsInstantiateAtDestination() ? player.transform.position : gameObject.transform.position, Quaternion.identity);
        attackObject.SetPositions(gameObject.transform.position, player.transform.position);
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        onCooldown = false;
    }
}
