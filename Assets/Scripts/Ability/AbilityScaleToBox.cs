using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
public class AbilityScaleToBox : MonoBehaviour
{
    [SerializeField] Vector3 dimensions = new Vector3(10f, 2f, 7f);

    void Start()
    {
        transform.localScale = dimensions;
    }
}
