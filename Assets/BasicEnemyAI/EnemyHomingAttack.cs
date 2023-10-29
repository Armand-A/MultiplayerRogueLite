using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHomingAttack : MonoBehaviour
{
    private NavMeshAgent bullet;
    [SerializeField] private float moveSpeed = 30f;
    private bool lockedOn;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        lockedOn = false;
        bullet = GetComponent<NavMeshAgent>();
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(!lockedOn)
        {
            Move();
        }
        else if (lockedOn)
        {
            bullet.SetDestination(target.transform.position);
        }
    }

    void Move()
    {
        transform.position = transform.position + moveSpeed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && lockedOn ==  false)
        {
            gameObject.GetComponent<SphereCollider>().enabled = false;
            target = other.gameObject;
            lockedOn = true;
        }
        else if(other.gameObject.tag == "Player" && lockedOn == true)
        {
            Debug.Log("Hit Home!");
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
