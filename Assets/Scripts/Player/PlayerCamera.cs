using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [Tooltip("To orientate player object's movement based on camera direction")]
    public Transform Orientation;
    [Tooltip("Player object container")]
    public Transform Player; //
    [Tooltip("Player capsule/player model")]
    public Transform PlayerObj; // To rotate object to face same direction as camera when moving
    public Rigidbody Rb;
    [Tooltip("Camera's speed of rotation")]
    public float RotationSpeed = 2f;

    public void UpdateCamera(Vector2 playerMovement)
    {
        // camera rotation orientation
        Vector3 viewDir = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        Orientation.forward = viewDir.normalized;

        float horizontalInput = 0;
        float verticalInput = 0;
        horizontalInput = playerMovement.x;
        verticalInput = playerMovement.y;
        Vector3 inputDir = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
            PlayerObj.forward = Vector3.Slerp(PlayerObj.forward, inputDir.normalized, Time.deltaTime * RotationSpeed);
    }
}
