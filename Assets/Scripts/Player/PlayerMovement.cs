using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

/// <summary>
/// Reference
///     https://youtu.be/f473C43s8nE
///     https://youtu.be/UCwwn2q4Vys?list=PLO8cZuZwLyxNpkKDdu7I5K6iIpWLh9tbt
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rigidBody;

    /// <summary>
    /// WASD Movement variables
    /// </summary>
    [Header("Horizontal Player Movement")]
    [Tooltip("Movement input vector")]
    public Vector2 MoveVector = Vector2.zero;
    [Tooltip("Object that help orient player movement direction based on camera")]
    public Transform Orientation;
    [Tooltip("Movement direction")]
    [SerializeField]
    private Vector3 MoveDir;
    [Tooltip("Base speed force value")]
    public float BaseSpeed = 250.0f;
    [Tooltip("Player rigidbody mass")]
    public float PlayerMass = 5.0f;
    [Tooltip("Base speed value")]
    public float SprintMultiplyer = 2.0f;
    [Tooltip("Sprinting input")]
    public float SprintBool = 0.0f;
    [Tooltip("Speed Multiplyer")]
    public float SpeedBoost = 1.0f;
    [Tooltip("Ground Drag for simulating air resistance when running")]
    public float GroundDrag = 3.0f;
    [Tooltip("How fast player's movement should be mid air")]
    public float AirMultiplyer = 0.1f;
    [Tooltip("Multiplyer for speed decrese during combat")]
    public float CombatSpeedMultiplyer = 0.5f;

    /// <summary>
    /// Vertical Jump movement variables
    /// </summary>
    ///
    [Header("Vertical Player Movement")]
    [Space(10)]
    [Tooltip("Jumping input")]
    public float JumpBool = 0.0f;
    [Tooltip("Able to Jump")]
    [SerializeField] bool CanJump = true;
    [Tooltip("The force of jump")]
    [SerializeField] float JumpForce = 10f;
    [Tooltip("Gravity value for character")]
    [SerializeField] float Gravity = -15.0f;
    [Tooltip("Interval between jumps")]
    [SerializeField] float JumpIntervalCD = 0.5f;
    [Tooltip("Time required before being able to jump again. Set to 0f to instantly jump again")]
    [SerializeField] float JumpCD = 0.1f;
    [Tooltip("Number of jumps available")]
    [SerializeField] int JumpRemaining = 2;
    [Tooltip("Number of jumps allowed")]
    [SerializeField] int MaxJump = 2;

    [Header("Stair/Slope Movement")]
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayLower;
    [SerializeField] float stepHeight = 0.3f;
    [SerializeField] float stepSmooth = 2f;

    /// <summary>
    /// Grounded checking variables
    /// </summary>
    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not")]
    [SerializeField] bool Grounded = false;
    [Tooltip("How deep should the raycast check for ground")]
    [SerializeField] float GroundedOffset = 0.1f;
    [Tooltip("The radius of the ground raycast detection range")]
    [SerializeField] float GroundedRadius = 0.1f;
    [Tooltip("Height of character")]
    [SerializeField] float PlayerHeight = 2f;
    [Tooltip("What layers the character can jump off of")]
    [SerializeField] LayerMask GroundLayers;


    [Header("Abilities")]
    [Tooltip("Dash input")]
    public float DashBool = 0.0f;
    [Tooltip("Dash input")]
    [SerializeField] float DashForce = 200.0f;
    [Tooltip("Dash Cooldown")]
    [SerializeField] float DashCD = 2.0f;
    [Tooltip("Dash Remaining")]
    [SerializeField] int DashRemaining = 2;
    [Tooltip("Max dash stored")]
    [SerializeField] int MaxDash = 2;
    [Tooltip("Dash action cost")]
    [SerializeField] int DashCost = 2;

    [Header("Other")]
    [SerializeField] PlayerCamera _playerCamera;
    private PlayerData _playerData;

    private CooldownTimer _jumpCDTimer;
    private CooldownTimer _jumpIntervalTimer;
    private CooldownTimer _dashCDTimer;

    private int _jumpCount = 0;
    private float _speed = 0.0f; //Final calculated speed value
    private bool _combatMode;
    
    /// <summary>
    /// Initialization of variables before game is loaded
    /// </summary>
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.mass = PlayerMass;
        _rigidBody.freezeRotation = true;

        _jumpCDTimer = new CooldownTimer(JumpCD);
        _jumpIntervalTimer = new CooldownTimer(JumpIntervalCD);
        _dashCDTimer = new CooldownTimer(DashCD);

        _playerData = GetComponent<PlayerData>();
    }

    /// <summary>
    /// Attaching cooldown event handlers
    /// </summary>
    private void OnEnable()
    {
        _jumpCDTimer.TimerCompleteEvent+= JumpRecharge;
        _dashCDTimer.TimerCompleteEvent += DashReset;
    }

    /// <summary>
    /// Removing cooldown event handlers just in case
    /// </summary>
    private void OnDisable()
    {
        _jumpCDTimer.TimerCompleteEvent -= JumpRecharge;
        _dashCDTimer.TimerCompleteEvent -= DashReset;
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
    /// All player movement updates
    /// </summary>
    private void FixedUpdate()
    {
        CheckCombatMode();
        // Camera function
        _playerCamera.UpdateCamera(MoveVector);

        // Normal Movements
        Movement();
        SpeedControl();

        //Special Movement
        Dash();
        _dashCDTimer.Update(Time.deltaTime);

        /* Jump Mechanic functions
        _jumpCDTimer.Update(Time.deltaTime);
        _jumpIntervalTimer.Update(Time.deltaTime);
        Jump();
        */
        GroundedCheck();
        GravityControl();
    }

    public void CheckCombatMode()
    {
        _combatMode = _playerData.CombatMode;
    }

    /// <summary>
    /// Jump control
    /// </summary>
    private void Jump()
    {
        // Adjust this for double jump
        if ((Grounded || _jumpCount < MaxJump) && JumpRemaining > 0 && !_jumpIntervalTimer.IsActive)
            CanJump = true;
        else
            CanJump = false;

        if(JumpBool > 0 && CanJump)
        {
            JumpRemaining--;
            _jumpCount++;
            _rigidBody.velocity = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);
            _rigidBody.AddForce(transform.up * (JumpForce), ForceMode.Impulse);
            _jumpIntervalTimer.Start();
        }
    }

    /// <summary>
    /// Applies gravity
    /// </summary>
    private void GravityControl()
    {
        // Do I need this? Might test later
        if (!Grounded)
            _rigidBody.AddForce(new Vector3(0, Gravity, 0));
    }

    /// <summary>
    /// Reset or add Jumping remaining
    /// </summary>
    private void JumpRecharge()
    {
        if (JumpRemaining < MaxJump)
            JumpRemaining++;
    }
    
    /// <summary>
    /// Check if Character is on a ground of somekind
    /// </summary>
    private void GroundedCheck()
    {
        // Sphere radius method, better with uneven ground (Require layers for ground)
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - PlayerHeight * 0.5f + GroundedRadius, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        
        //Raycast method to detect ground
        // Grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + GroundedOffset, GroundLayers);
        
        _rigidBody.drag = Grounded ? GroundDrag : 1;
        if (Grounded)
        {
            _jumpCount = 0;
            _jumpIntervalTimer.Pause();
            if (!_jumpCDTimer.IsActive && JumpRemaining < MaxJump)
                _jumpCDTimer.Start();
        }
    }

    private void SlopeCheck()
    {
        
    }

    /// <summary>
    /// Character movement
    /// </summary>
    private void Movement()
    {
        //Makes sure the orientation of the movement is based on direction of where the camera is facing
        MoveDir = Orientation.forward * MoveVector.y + Orientation.right * MoveVector.x;

        // Removing velocity when player not touching movement keys
        Vector3 flatVel = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);
        if (flatVel.magnitude <= 10f && MoveVector == Vector2.zero)
        {
            SprintBool = 0;
            _speed = 0;
            _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, 0);
        }
        else
        {
            // Speed calculation BaseSpeed or SprintSpeed plus SpeedBoost
            _speed = (SprintBool > 0 ? BaseSpeed * SprintMultiplyer : BaseSpeed) + SpeedBoost;
            if (_combatMode)
                _speed *= CombatSpeedMultiplyer;
        }

        //Calculates in midair movement
        _speed = Grounded ? _speed : _speed * AirMultiplyer;
        _rigidBody.AddForce(MoveDir.normalized * _speed);
    }

    /// <summary>
    /// Speed limiter
    /// Maybe not needed?
    /// </summary>
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);
        if (flatVel.magnitude > _speed && !_dashCDTimer.IsActive)
        {
            Vector3 limitedVel = flatVel.normalized * _speed;
            _rigidBody.velocity = new Vector3(limitedVel.x, _rigidBody.velocity.y, limitedVel.z);
        }
    }

    private void Dash()
    {
        if (DashBool > 0 && (DashRemaining > 0 || !_combatMode))
        {   
            if (_playerData.UpdateAction(-DashCost))
                _rigidBody.AddForce(MoveDir.normalized * DashForce , ForceMode.Impulse);

            if (_combatMode)
                DashRemaining--;
            
        }
        DashBool = 0;

        if (!_dashCDTimer.IsActive && DashRemaining < MaxDash)
        {
            _dashCDTimer.Start();
        }
    }

    /// <summary>
    /// Reset Dash cooldown
    /// </summary>
    private void DashReset()
    {
        if (DashRemaining < MaxDash)
        {
            DashRemaining++;
        }
    }
}
