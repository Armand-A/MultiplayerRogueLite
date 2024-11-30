using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    public Transform BaseObject;
    private Transform Camera;

    private void Awake()
    {
        if (Camera == null)
        {
            Camera = GameObject.FindWithTag("MainCamera").GetComponent<Transform>();
        }
    }

    private void LateUpdate()
    {
        if (transform == null)
        {
            return;
        }
        transform.position = new Vector3(BaseObject.transform.position.x, BaseObject.transform.position.y + 1.3f, BaseObject.transform.position.z);
        transform.LookAt(transform.position + Camera.forward);
    }
}
