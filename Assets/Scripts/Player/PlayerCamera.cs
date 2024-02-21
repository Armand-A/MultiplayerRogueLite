using System;
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
    [Tooltip("Player object container")]
    public Transform Player; //
    [Tooltip("Player capsule/player model")]
    public Transform PlayerObj; // To rotate object to face same direction as camera when moving

    [Header("General Configs")]
    [Tooltip("Speed of player model reorienting towards moving direction")]
    public float RotationSpeed = 5f;
    [Tooltip("Camera View Style")]
    public CameraStyle CurrentCamStyle;

    [Header("Normal View Camera Configs")]
    [Tooltip("Reference to Normal View Camera")]
    public GameObject NormalViewCam;

    [Header("Aim Camera Configs")]
    [Tooltip("Reference to Aim Camera")]
    public GameObject AimCam;
    [Tooltip("Reference to Focus of Aim Camera")]
    public GameObject AimFocus;
    [Tooltip("Sensitivity/Speed of camera rotation in Aim mode"), SerializeField]
    float aimSensitivity = 5f;
    [Tooltip("Max angle of vertical rotation"), Range(0f, 90f), SerializeField]
    float aimVerticalRotationLimit = 60f;

    public Vector2 LookDelta { get; set; }
    
    public enum CameraStyle
    {
        Basic,
        Aim, 
    }

    // Makes sure player's movement is based off of camera's orientation
    public void UpdateCamera(Vector2 playerMovement)
    {
        if (CurrentCamStyle == CameraStyle.Basic)
        {
            // camera rotation orientation
            Vector3 viewDir = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
            Orientation.forward = viewDir.normalized;

            float horizontalInput = playerMovement.x;
            float verticalInput = playerMovement.y;
            Vector3 inputDir = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                PlayerObj.forward = Vector3.Slerp(PlayerObj.forward, inputDir.normalized, Time.deltaTime * RotationSpeed);
        }
        else if (CurrentCamStyle == CameraStyle.Aim)
        {
            Vector2 rotInput = LookDelta;

            Vector3 rot = PlayerObj.transform.eulerAngles;
            rot.y += rotInput.x * aimSensitivity * Time.deltaTime;
            PlayerObj.transform.rotation = Quaternion.Euler(rot);
            Orientation.transform.rotation = Quaternion.Euler(rot);

            if (AimFocus != null)
            {
                rot = AimFocus.transform.localRotation.eulerAngles;
                rot.x -= rotInput.y * aimSensitivity * Time.deltaTime;
                if (rot.x > 180f) rot.x -= 360f;
                rot.x = Mathf.Clamp(rot.x, -aimVerticalRotationLimit, aimVerticalRotationLimit);
                AimFocus.transform.localRotation = Quaternion.Euler(rot);
            }
        }
    }

    public void SwitchCameraStyle(CameraStyle camStyle)
    {
        if (camStyle == CurrentCamStyle) return;

        NormalViewCam.SetActive(false);
        AimCam.SetActive(false);

        CurrentCamStyle = camStyle;
        switch (CurrentCamStyle)
        {
            case CameraStyle.Basic:
                NormalViewCam.SetActive(true);
                break;
            case CameraStyle.Aim:
                AimCam.SetActive(true);
                break;
        }
    }
}
