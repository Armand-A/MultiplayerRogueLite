using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbDespawnAfterDisplacement : MonoBehaviour
{
    [SerializeField] float maxDisplacement = 15f;

    Ability _ability;

    private void Start()
    {
        _ability = GetComponent<Ability>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _ability.SrcPos) > maxDisplacement)
        {
            Destroy(gameObject);
        }
    }
}
