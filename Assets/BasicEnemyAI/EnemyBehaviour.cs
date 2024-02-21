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
    public EnemyData enemyData;

    EnemyData _enemyData;

    // Start is called before the first frame update
    void Start()
    {
        _enemyData = gameObject.GetComponent<EnemyData>();

        health = _enemyData.ResourceMan.Health;
        actions = _enemyData.ResourceMan.Action;
        enemy = GetComponent<NavMeshAgent>();
        enemyData = GetComponent<EnemyData>();
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
                if (actions.Remove(Attack.ActionCost))
                {
                    onCooldown = true;
                    //actions.Value = actions.Value - Attack.ActionCost;
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
        Ability abilityObject = Instantiate(Random.Range(1, 3) == 1 ? Attack : Attack2, Attack is AnywhereAbility ? player.transform.position : gameObject.transform.position, Quaternion.identity);
        abilityObject.Initialize(gameObject.transform.position, enemyData, player.transform.position, new Ray(transform.position, (player.transform.position - transform.position).normalized), player);

        // TODO: gameObject y position keeps falling, skewing srcPos of the instantiated ability
        // Debug.Log(gameObject.transform.position.y.ToString() + " " + player.transform.position.y);
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        onCooldown = false;
    }
}