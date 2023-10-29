using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyBehaviour : MonoBehaviour
{
    public bool playerDetected;
    public GameObject player;
    private bool onCooldown;
    public NavMeshAgent enemy;
    public bool ranged;
    [SerializeField] private AttackScriptableObject Attack;
    [SerializeField] private AttackScriptableObject Attack2;
    public int Health;
    [SerializeField] private float maxActions;
    [SerializeField] private float currActions;
    private bool onActionRechargeCooldown;

    // Start is called before the first frame update
    void Start()
    {
        onActionRechargeCooldown = false;
        currActions = maxActions;
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

        if(currActions != maxActions && !onActionRechargeCooldown)
        {
            onActionRechargeCooldown = true;
            currActions++;
            StartCoroutine(ActionRechargeCooldown());

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.speed = 0;
            if (!onCooldown)
            {
                if (Attack.ActionCost <= currActions)
                {
                    onCooldown = true;
                    currActions = currActions - Attack.ActionCost;
                    StartCoroutine(Cooldown());
                    EAttack();
                }
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

    void EAttack()
    {
        if (!ranged)
        {
            int attack = Random.Range(1, 3);
            if (attack == 1)
            {
                AttackBehaviour attackObject = Instantiate(Attack.AttackBehaviour, Attack.AttackBehaviour.GetIsInstantiateAtDestination() ? player.transform.position : gameObject.transform.position, Quaternion.identity);
                attackObject.SetPositions(gameObject.transform.position, player.transform.position);
            }
            else if (attack == 2)
            {
                AttackBehaviour attackObject = Instantiate(Attack2.AttackBehaviour, Attack2.AttackBehaviour.GetIsInstantiateAtDestination() ? player.transform.position : gameObject.transform.position, Quaternion.identity);
                attackObject.SetPositions(gameObject.transform.position, player.transform.position);
            }
        }
        else if (ranged)
        {
            AttackBehaviour attackObject = Instantiate(Attack.AttackBehaviour, Attack.AttackBehaviour.GetIsInstantiateAtDestination() ? player.transform.position : gameObject.transform.position, Quaternion.identity);
            attackObject.SetPositions(gameObject.transform.position, player.transform.position);
        }
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        onCooldown = false;
    }

    IEnumerator ActionRechargeCooldown()
    {
        yield return new WaitForSeconds(1);
        onActionRechargeCooldown = false;
    }
}
