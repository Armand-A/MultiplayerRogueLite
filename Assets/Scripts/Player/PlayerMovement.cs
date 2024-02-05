using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Transform Orientation;
    [Tooltip("Movement direction")]
    [SerializeField] Vector3 MoveDir;
    [Tooltip("Base speed force value")]
    [SerializeField] float BaseSpeed = 250.0f;
    [Tooltip("Player rigidbody mass")]
    [SerializeField] float PlayerMass = 5.0f;
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

    [Header("Stair/Slope Movement")]
    [Tooltip("Allowed height for stepping up")]
    [SerializeField] private float stepHeight = 0.5f;
    [Tooltip("How smooth stepping up should be")]
    [SerializeField] private float stepSmooth = 2f;
    [Tooltip("Maximum allowed slope to climb")]
    [SerializeField] private float maxSlopeAngle = 70f;
    private Collider stepTarget;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    [Tooltip("Collider to visualize and position the stair detecter")]
    [SerializeField] private GameObject stairDetector;

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

    /// <summary>
    /// Grounded checking variables
    /// </summary>
    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not")]
    [SerializeField] bool Grounded = false;
    //[Tooltip("How deep should the raycast check for ground")]
    //[SerializeField] float GroundedOffset = 0.1f;
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

        GroundedCheck();
        GravityControl();

        /* Jump Mechanic functions
        _jumpCDTimer.Update(Time.deltaTime);
        _jumpIntervalTimer.Update(Time.deltaTime);
        Jump();
        JumpCDControl();
        */
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

    private void JumpCDControl()
    {
        if (Grounded)
        {
            _jumpCount = 0;
            _jumpIntervalTimer.Pause();
            if (!_jumpCDTimer.IsActive && JumpRemaining < MaxJump)
                _jumpCDTimer.Start();
        }
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
    /// Applies gravity
    /// </summary>
    private void GravityControl()
    {
        // Do I need this? Might test later
        if (!OnSlope() && !OnClimbStep())
            _rigidBody.AddForce(new Vector3(0, Gravity, 0));
    }
    
    /// <summary>
    /// Check if Character is on a ground of somekind
    /// </summary>
    private void GroundedCheck()
    {
        // Sphere radius method, better with uneven ground (Require layers for ground)
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - PlayerHeight * 0.5f, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        
        //Raycast method to detect ground
        // Grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + GroundedOffset, GroundLayers);
        
        if (OnClimbStep())
        {
            Grounded = true;
        }
        _rigidBody.drag = Grounded ? GroundDrag : 1;
    }

    private bool OnClimbStep()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - PlayerHeight * 0.5f + stepHeight + 0.01f , stairDetector.transform.position.z);
        // to see if it is about to hit anything.
        Collider[] collisions = Physics.OverlapSphere(spherePosition, stepHeight, GroundLayers, QueryTriggerInteraction.Ignore);

        // Get the step with the greatest difference
        float highestStep = 0;

        if (collisions.Length != 0)
        {
            foreach (Collider collision in collisions)
            {
                // height of the top of the detected object compared to the foot of the player
                float stepHeightDifference = collision.bounds.center.y + collision.bounds.size.y/2 - (transform.position.y - PlayerHeight * 0.5f);
                if (highestStep <=  stepHeight)
                {
                    // Debug.Log("Step upable");
                    // if (stepHeightDifference > highestStep)
                    // {
                    //     highestStep = stepHeightDifference;
                    //     stepTarget = collision;
                    // }
                    
                    return true;
                }
            }
            if (highestStep > 0)
                return true;
        }
        
        return false;
    }

    private Vector3 GetStepDirection()
    {
        if (stepTarget == null)
        {
            return MoveDir;
        }
        Vector3 stepTargetTop = stepTarget.bounds.center + new Vector3(0, stepTarget.bounds.size.y/2, 0);
        Vector3 playerBottom = new Vector3(transform.position.x, (transform.position.y - PlayerHeight * 0.5f), transform.position.z);
        Vector3 stepAngle = stepTargetTop - playerBottom;
        return Vector3.ProjectOnPlane(MoveDir, stepAngle.normalized).normalized;
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, PlayerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        //Debug.Log(slopeHit.normal);
        return Vector3.ProjectOnPlane(MoveDir, slopeHit.normal).normalized;
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
        
        // Different force distribution for slope movement
        if (OnSlope())
        {
            //Debug.Log("On slope");
            _rigidBody.AddForce(GetSlopeMoveDirection() * _speed);
        }
        else if (OnClimbStep() && MoveDir != Vector3.zero)
        {
            // Vector3 temp = GetStepDirection();
            // Vector3 climbDirection = new Vector3(temp.x, -temp.y, temp.z);
            // //Debug.Log(stepTarget + " : " + climbDirection);
            // _rigidBody.AddForce(new Vector3(climbDirection.x * _speed, climbDirection.y * _speed, climbDirection.z * _speed));
            _rigidBody.position += new Vector3(0f, stepSmooth * Time.deltaTime, 0f);
            _rigidBody.AddForce(MoveDir.normalized * _speed);
        }
        else
        {
            _rigidBody.AddForce(MoveDir.normalized * _speed);
        }

    }

    /// <summary>
    /// Speed limiter
    /// Maybe not needed?
    /// </summary>
    private void SpeedControl()
    {
        if (OnSlope() || OnClimbStep())
        {
            if (_rigidBody.velocity.magnitude > _speed)
                _rigidBody.velocity = _rigidBody.velocity.normalized * _speed;
        }
        else
        {
            Vector3 flatVel = new Vector3(_rigidBody.velocity.x, 0f, _rigidBody.velocity.z);
            if (flatVel.magnitude > _speed && !_dashCDTimer.IsActive)
            {
                Vector3 limitedVel = flatVel.normalized * _speed;
                _rigidBody.velocity = new Vector3(limitedVel.x, _rigidBody.velocity.y, limitedVel.z);
            }
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
