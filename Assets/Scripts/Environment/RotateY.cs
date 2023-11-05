using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    [SerializeField] bool useRandomSpeed = false;
    [SerializeField] float randomSpeedMin = -30f;
    [SerializeField] float randomSpeedMax = 30f;
    [SerializeField] float speed = 30f;

    float realSpeed;

    private void OnValidate()
    {
        UpdateSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * realSpeed * Time.deltaTime);
    }

    void UpdateSpeed()
    {
        if (useRandomSpeed)
        {
            realSpeed = Random.Range(randomSpeedMin, randomSpeedMax);
        }
        else
        {
            realSpeed = speed;
        }
    }
}
