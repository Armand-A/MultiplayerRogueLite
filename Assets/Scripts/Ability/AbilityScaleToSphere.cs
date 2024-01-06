using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbilityScaleToSphere : MonoBehaviour
{
    [SerializeField] float radius = .5f;

    private void Start()
    {
        transform.localScale = 2 * radius * Vector3.one;
    }
}
