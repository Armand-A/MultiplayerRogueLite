using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbilityScaleToCylinder : MonoBehaviour
{
    [SerializeField] float radius = 4.5f;
    [SerializeField] float height = 1f;

    void Start()
    {
        transform.localScale = new Vector3(radius * 2, height, radius * 2);
    }
}
