using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerNetwork : NetworkBehaviour {
    [SyncVar]
    public Vector3 syncPosition;
    [SyncVar]
    public Quaternion syncRotation;

    public GameObject myCamera;
    public float lerpRate = 15;

    private void FixedUpdate()
    {
        TransmitPosition();
        Lerp();
    }
    void Lerp()
    {
        if (!hasAuthority) return;
        transform.position = Vector3.Lerp(transform.position, syncPosition, Time.deltaTime * lerpRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, syncRotation, Time.deltaTime * lerpRate);
    }

    [ClientCallback]
    void TransmitPosition()
    {
        if(myCamera != null)
        CmdProvidePositionToServer(transform.position, myCamera.transform.rotation);
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 pos, Quaternion rot)
    {
        syncPosition = pos;
        syncRotation = rot;
    }
}