using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reference
///     - https://www.youtube.com/watch?v=UCwwn2q4Vys
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    [Header("References")]
    [Tooltip("To orientate player object's movement based on camera direction")]
    public Transform Orientation;
    [Tooltip("Where the player would look at during combat")]
    public Transform CombatLookAt;
    [Tooltip("Player object container")]
    public Transform Player; //
    [Tooltip("Player capsule/player model")]
    public Transform PlayerObj; // To rotate object to face same direction as camera when moving
    [Tooltip("Player rigidbody")]
    public Rigidbody Rb;
    [Tooltip("Top Down Camera")]
    public GameObject Crosshair;

    [Header("Values")]
    [Tooltip("Camera's speed of rotation")]
    public float RotationSpeed = 5f;
    [Tooltip("Camera View Style")]
    public CameraStyle CurrentCamStyle;
    [Tooltip("Normal View Camera")]
    public GameObject NormalViewCam;
    [Tooltip("Combat Camera")]
    public GameObject CombatCam;
    [Tooltip("Top Down Camera")]
    public GameObject TopDownCam;
    
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }

    // Makes sure player's movement is based off of camera's orientation
    public void UpdateCamera(Vector2 playerMovement)
    {
        if (CurrentCamStyle == CameraStyle.Basic || CurrentCamStyle == CameraStyle.Topdown)
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
        else
        {
            // camera rotation orientation to face CombatLookAt Gameobject
            Vector3 combatViewDir = CombatLookAt.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
            Orientation.forward = combatViewDir.normalized;

            PlayerObj.forward = combatViewDir.normalized;
        }
    }

    //for switching different camera styles if needed
    public void SwitchCameraStyle()
    {
        NormalViewCam.SetActive(false);
        CombatCam.SetActive(false);
        TopDownCam.SetActive(false);
        Crosshair.SetActive(false);

        // if (newStyle == CameraStyle.Basic)
        //     NormalViewCam.SetActive(true);
        // if (newStyle == CameraStyle.Combat)
        //     CombatCam.SetActive(true);
        // if (newStyle == CameraStyle.Topdown)
        //     TopDownCam.SetActive(true);

        if (CurrentCamStyle == CameraStyle.Basic)
        {
            CombatCam.SetActive(true);
            Crosshair.SetActive(true);
            CurrentCamStyle = CameraStyle.Combat;
        }
        else if (CurrentCamStyle == CameraStyle.Combat)
        {
            TopDownCam.SetActive(true);
            CurrentCamStyle = CameraStyle.Topdown;

        }
        else if (CurrentCamStyle == CameraStyle.Topdown)
        {
            NormalViewCam.SetActive(true);
            CurrentCamStyle = CameraStyle.Basic;
        }
        
        // CurrentCamStyle = newStyle;
    }
}
