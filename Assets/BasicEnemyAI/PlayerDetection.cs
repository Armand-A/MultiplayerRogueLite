using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDetection : MonoBehaviour
{
    [Header("Sensing Suite")]
    public bool LOS;
    private EnemyBehaviour enemyBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviour = this.transform.parent.GetComponent<EnemyBehaviour>();
        LOS = false;
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBehaviour.playerDetected == false && gameObject.GetComponent<SphereCollider>().enabled == false)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            enemyBehaviour.player = other.gameObject;
            enemyBehaviour.playerDetected = true;
            Debug.Log("I see the player");
            gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
