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
    [SerializeField] private Ability Attack;
    [SerializeField] private Ability Attack2;
    [SerializeField] private Action actions;
    public Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        actions = GetComponent<Action>();
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
        if(health.Value <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.speed = 0;
            if (!onCooldown)
            {
                if (Attack.ActionCost <= actions.Value)
                {
                    onCooldown = true;
                    actions.Value = actions.Value - Attack.ActionCost;
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
        Ability abilityObject = Instantiate(Random.Range(1, 3) == 1 ? Attack : Attack2, Attack.IsInstantiateAtDestination ? player.transform.position : gameObject.transform.position, Quaternion.identity);
        abilityObject.Initialize(gameObject.transform.position, player.transform.position, false);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        onCooldown = false;
    }
}
