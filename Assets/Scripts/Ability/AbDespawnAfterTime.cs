using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability)), AddComponentMenu("Ability/Despawn/Despawn After Time")]
public class AbDespawnAfterTime : MonoBehaviour
{
    [SerializeField] private float timeBeforeDespawn = 3f;

    private void Start()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        Destroy(gameObject); // TODO: object pooling
    }
}
