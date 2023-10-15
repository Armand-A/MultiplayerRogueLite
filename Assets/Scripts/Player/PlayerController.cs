using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;

    /// <summary>
    /// WASD Movement variables
    /// </summary>
    [Header("Horizontal Player Movement")]
    [Tooltip("Movement input value")]
    public Vector2 MoveVector = Vector2.zero;
    [Tooltip("Object that help orient player movement direction based on camera")]
    public Transform Orientation;
    [Tooltip("Movement direction")]
    public Vector3 MoveDir;
    [Tooltip("Base speed value")]
    public float BaseSpeed = 10.0f;
    [Tooltip("Base speed value")]
    public float SprintMultiplyer = 2.0f;
    [Tooltip("Sprinting check")]
    public float SprintBool = 0.0f;
    [Tooltip("Speed Multiplyer")]
    public float SpeedBoost = 1.0f;
    [Tooltip("Ground Drag for simulating air resistance when running")]
    public float GroundDrag = 5.0f;
    private float _speed = 0.0f; //Final calculated speed value

    /// <summary>
    /// Vertical Jump movement variables
    /// </summary>
    [Header("Vertical Player Movement")]
    [Space(10)]
    [Tooltip("The force of jump")]
    public float JumpForce = 10f;
    [Tooltip("Gravity value for character")]
    public float Gravity = -15.0f;
    [Tooltip("Jumping check")]
    public float JumpBool = 0.0f;
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpCD = 0.1f;
    [Tooltip("Number of jumps available")]
    public int JumpCount = 1;
    [Tooltip("Number of jumps allowed")]
    public int MaxJump = 2;
    [Tooltip("Mid air slowing multiplyer")]
    public float AirDrag = 8f;

    /// <summary>
    /// Grounded checking variables
    /// </summary>
    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not")]
    public bool Grounded = false;
    [Tooltip("How deep should the raycast check for ground")]
    public float GroundedOffset = 0.1f;
    [Tooltip("Height of character")]
    public float PlayerHeight = 2f;
    [Tooltip("What layers the character can jump off of")]
    public LayerMask GroundLayers;

    /// <summary>
    /// Player Camera script from player camera
    /// </summary>
    [Header("Player Camera")]
    public PlayerCamera _playerCamera;
    
    /// <summary>
    /// Initialization of variables before game is loaded
    /// </summary>
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.freezeRotation = true;
    }

    /// <summary>
    /// At the beginning of the game
    /// Remove cursors
    /// </summary>
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// FixedUpdate instead of update because rigidbody works better this way
    /// </summary>
    private void FixedUpdate()
    {
        _playerCamera.UpdateCamera(MoveVector);
        Movement();
        //SpeedControl();
        GroundedCheck();
        Jump();
        GravityControl();
    }

    /// <summary>
    /// Jump control
    /// </summary>
    private void Jump()
    {   
        bool CanJump = false;

        // Adjust this for double jump
        if (Grounded)
        {
            CanJump = true;
        }
        if(JumpBool > 0 && CanJump)
        {
            JumpCount--;
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);
            _rigidBody.AddForce(transform.up * (JumpForce), ForceMode.Impulse);
            Invoke(nameof(ResetJump), JumpCD);
        }
    }

    /// <summary>
    /// Applies gravity
    /// </summary>
    private void GravityControl()
    {
        // Do I need this? Might test later
        if (!Grounded)
        {
            _rigidBody.AddForce(new Vector3(0, Gravity, 0));
        }
    }

    /// <summary>
    /// Reset or add Jumping count
    /// </summary>
    private void ResetJump()
    {
        if (JumpCount < MaxJump)
        {
            JumpCount++;
        }
    }
    
    /// <summary>
    /// Check if Character is capable of jumping
    /// </summary>
    private void GroundedCheck()
    {
        // set sphere position, with offset
        // Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        // Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        
        Grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + GroundedOffset, GroundLayers);
        _rigidBody.drag = Grounded ? GroundDrag : AirDrag;
    }

    /// <summary>
    /// Character movement
    /// </summary>
    private void Movement()
    {
        // Speed calculation BaseSpeed or SprintSpeed plus SpeedBoost
        _speed = (SprintBool > 0 ? BaseSpeed * SprintMultiplyer : BaseSpeed)  + SpeedBoost;
        //Makes sure the orientation of the movement is based on direction of where the camera is facing
        MoveDir = Orientation.forward * MoveVector.y + Orientation.right * MoveVector.x;

        //Calculates in midair movement
        _speed = Grounded ? _speed : _speed * AirDrag;
        _rigidBody.AddForce(MoveDir.normalized * _speed);
    }

    /// <summary>
    /// Speed limiter
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);
        if (flatVel.magnitude > _speed)
        {
            Vector3 limitedVel = flatVel.normalized * _speed;
            _rigidBody.velocity = new Vector3(limitedVel.x, _rigidBody.velocity.y, limitedVel.z);
        }
    }
}
