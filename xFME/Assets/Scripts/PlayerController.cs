using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    /* Determine where we can or no move/sprint/crouching or jumping */
    public bool CanMove { get; private set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey( sprintKey );
    private bool IsJumping => Input.GetKeyDown( jumpKey ) && characterController.isGrounded;
    private bool IsCrouching => Input.GetKeyDown( crouchKey ) && !duringCrouchAnimation && characterController.isGrounded;

    [Header("Movement Functions")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadBob = true;
    [SerializeField] private bool canSlideOnSlopes = true;
    [SerializeField] private bool canZoom = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool useFootsteps = true;
    [SerializeField] private bool useStamina = true;

    [Header("Player Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode zoomKey = KeyCode.Z;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.0f; 
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float slopeSpeed = 8f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 110)] private float upperLookLimit = 56.0f;
    [SerializeField, Range(1, 110)] private float lowerLookLimit = 56.0f;

    [Header("Player Parameters")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float staminaUseMultiplier = 5;
    [SerializeField] private float timeBeforeRegenStart = 3;
    [SerializeField] private float timeBeforeStaminaRegenStart = 5;
    [SerializeField] private float healthValueIncrement = 1;
    [SerializeField] private float staminaValueIncrement = 2;
    [SerializeField] private float healthTimeIncrement = 0.1f;
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    private float currentHealth;
    private float currentStamina;
    private Coroutine regeneratingHealth;
    private Coroutine regeneratingStamina;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;
    public static Action<float> OnStaminaChange;

    [Header("Physics Parameters")]
    [SerializeField] private float gravity = 30.0f;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchCenter = new Vector3( 0, 0.5f, 0 );
    [SerializeField] private Vector3 standingCenter = new Vector3( 0, 0, 0 );
    private bool IsInternalCrouching;
    private bool duringCrouchAnimation;

    [Header("View Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    [Header("Zoom Parameters")]
    [SerializeField] private float timeToZoom = 0.3f;
    [SerializeField] private float zoomFOV = 30f;
    private float defaultFOV;
    private Coroutine zoomRoutine;

    [Header("Player Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private ObjectInteractible currentInteractable;

    [Header("Footstep Interaction")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;
    [SerializeField] private float sprintMultiplier = 0.6f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] defaultClip;
    [SerializeField] private AudioClip[] grassClip;
    [SerializeField] private AudioClip[] gravelClip;
    [SerializeField] private AudioClip[] metalClip;
    [SerializeField] private AudioClip[] tileClip;
    [SerializeField] private AudioClip[] waterClip;
    [SerializeField] private AudioClip[] woodClip;
    private float footstepTimer = 0;
    private float GetCurrentOfset => IsInternalCrouching ? baseStepSpeed * crouchStepMultiplier : IsSprinting ? baseStepSpeed * sprintMultiplier : baseStepSpeed;

    /* Sliding Movement goes here */
    private Vector3 hitPointNormal;
    private bool IsSliding
    {
        get
        {
            if ( characterController.isGrounded && Physics.Raycast( transform.position, Vector3.down, out RaycastHit slopeHit, 2f ) )
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle( hitPointNormal, Vector3.up ) > characterController.slopeLimit;
                //realSlopeDirection = Vector3.Cross(Vector3.Cross(hit.normal, gravityDirection), hit.normal);
            }
            else
                return false;
        }
    }
    /* End of Sliding code */

    /* Get the player camera */
    private Camera playerCamera;

    /*Get the CharacterController */
    private CharacterController characterController;

    /* Get our Animator from our player mesh */
    private Animator playerAnim;

    /* Animation hashes */
    private float animationPlayTransition = 0.15f;
    private int isSprintingHash;
    private int isCrouchingHash;
    private int isJumpingHash;
    private int jumpAnimation;
    private int crouchAnimation;
    private int moveX;
    private int moveZ;

    /* Our actual movement */
    private Vector3 moveDirection;
    private Vector2 currentInput;

    /* Clamp the rotation */
    private float rotationX = 0;

    /* Make us a singleton for easy access */
    public static PlayerController instance;

    // This function is called when the object becomes enabled and active.
    private void OnEnable()
    {
        OnTakeDamage += ApplyDamage;
    }

    // This function is called when the behaviour becomes disabled.
    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }

   // Awake is called when the script instance is being loaded.
    void Awake()
    {
        /* We are the player so make our singleton at us */
        instance = this;

        /* Get our camera has children */
        playerCamera = GetComponentInChildren<Camera>();

        /* Attach our camera o the player's mesh head bone */
        var animator = GetComponentInChildren<Animator>();
        playerCamera.transform.parent = animator.GetBoneTransform ( HumanBodyBones.Head );

        /* Get our Animator from our Player Mesh*/
        playerAnim = GetComponentInChildren<Animator>();

        /* Caches our animation names */
        moveX = Animator.StringToHash( "MoveX" );
        moveZ = Animator.StringToHash( "MoveZ" );

        /* Reference our animations */
        jumpAnimation = Animator.StringToHash( "Jump" );
        crouchAnimation = Animator.StringToHash( "Sneak Walk" );
        isSprintingHash = Animator.StringToHash( "isSprinting" );
        isCrouchingHash = Animator.StringToHash( "isCrouching" );
        isJumpingHash = Animator.StringToHash( "isJumping" );

        /* Set our default Y for viewbob */
        defaultYPos = playerCamera.transform.localPosition.y;

        /* Set our default FOV */
        defaultFOV = playerCamera.fieldOfView;

        /* Setup our default health */
        currentHealth = maxHealth;

        /* Setup our default stamina */
        currentStamina = maxStamina;

        /* Get our CharacterController */
        characterController = GetComponent<CharacterController>();

        /* Hide our cursor has we don't want it to be visible */
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* Only Execute our movement code if we can move */
        if ( CanMove )
        {
            /* Process our input */
            HandleMovementInput();

            /* Process our mouse look */
            HandleMouseLook();

            /* Process our jump movement only if we are allowed to do so */
            if ( canJump )
                HandleJumping();

            /* Process our crouch movement only if we are allowed to do so */
            if ( canCrouch )
                HandleCrouching();

            /* Process our crouch movement only if we are allowed to do so */
            if ( canUseHeadBob )
                HandleViewBob();

            /* Process our Zoom only if we are allowed to do so */
            if ( canZoom )
                HandleZoom();

            /* Play footsteps sound if we are allowed to do so */
            if ( useFootsteps )
                HandleFootsteps();

            /* Handles our interaction */
            if ( canInteract )
            {
                HandleInteractionCheck();
                HandleInteractionInput();
            }

            /* Handles our stamina regen */
            if ( useStamina )
                HandleStamina();

            /* Apply the final movement */
            ApplyFinalMovement();
        }
    }

    private void HandleMovementInput()
    {
        /* Check if we are sprinting or not and assign it to a variable */
        float movementState = ( IsInternalCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed );

        /*  Set our currentInput for Vertical/Horizontal */
        currentInput = new Vector2( movementState * Input.GetAxis( "Vertical" ), movementState * Input.GetAxis( "Horizontal" ) );

        float moveDirectionY = moveDirection.y;
        moveDirection = ( transform.TransformDirection( Vector3.forward ) * currentInput.x ) + ( transform.TransformDirection( Vector3.right ) * currentInput.y );
        moveDirection.y = moveDirectionY;

        /* Do our player animations */
        DoPlayerAnimations();
    }

    /* Executes our animation */
    private void DoPlayerAnimations()
    {
        playerAnim.SetFloat( moveX, moveDirection.x );
        playerAnim.SetFloat( moveZ, moveDirection.z );

        /* Check if we are walking/running or not */
        //bool isWalking = playerAnim.GetBool( isWalkingHash );
        //bool isRunning = playerAnim.GetBool( isRunningHash );

        /* If we are actually moving, set our animation to walking */
        /*if ( !isWalking && currentInput != Vector2.zero )
            playerAnim.SetBool( isWalkingHash, true );

        if ( isWalking && currentInput == Vector2.zero )
            playerAnim.SetBool( isWalkingHash, false );*/

        /* If we are actually moving, and we are running set our animation to running */
        /*if ( !isRunning && ( IsSprinting && currentInput != Vector2.zero ) )
            playerAnim.SetBool( isRunningHash, true );

        if ( isRunning && ( !IsSprinting && currentInput == Vector2.zero ) )
            playerAnim.SetBool( isRunningHash, false );*/
    }

    private void HandleFootsteps()
    {
        /* Make sure if we are actually in the ground */
        if ( !characterController.isGrounded )
            return;

        /* And we are actually moving */
        if ( currentInput == Vector2.zero )
            return;

        footstepTimer -= Time.deltaTime;

        if ( footstepTimer <= 0 )
        {
            if ( Physics.Raycast( playerCamera.transform.position, Vector3.down, out RaycastHit hit, 3f ) )
            {
                /* Determine our footstep sound based of our object tag */
                switch ( hit.collider.tag )
                {
                    default:
                    case "FootSteps/Concrete":
                        footstepAudioSource.PlayOneShot( defaultClip[ Random.Range( 0, defaultClip.Length - 1 ) ] );
                        break;
                    case "FootSteps/Grass":
                        footstepAudioSource.PlayOneShot( grassClip[ Random.Range( 0, grassClip.Length - 1 ) ] );
                        break;
                    case "FootSteps/Gravel":
                        footstepAudioSource.PlayOneShot( gravelClip[ Random.Range( 0, gravelClip.Length - 1 ) ] );
                        break;
                    case "FootSteps/Metal":
                        footstepAudioSource.PlayOneShot( metalClip[ Random.Range( 0, metalClip.Length - 1 ) ] );
                        break;
                    case "FootSteps/Tile":
                        footstepAudioSource.PlayOneShot( tileClip[ Random.Range( 0, tileClip.Length - 1 ) ] );
                        break;
                    case "FootSteps/Water":
                        footstepAudioSource.PlayOneShot( waterClip[ Random.Range( 0, waterClip.Length - 1 ) ] );
                        break;
                    case "FootSteps/Wood":
                        footstepAudioSource.PlayOneShot( woodClip[ Random.Range( 0, woodClip.Length - 1 ) ] );
                        break;
                }
                
            }

            footstepTimer = GetCurrentOfset;
        }
    }

    private void ApplyDamage( float dmg )
    {
        //TODO: Calculate the damage based on the Hitboxes!
        currentHealth -= dmg;
        
        /* For our listeners, send the action for when we take damage */
        OnDamage?.Invoke( currentHealth ); 

        if ( currentHealth <= 0 )
            KillPlayer();
        else if ( regeneratingHealth != null )
            StopCoroutine( regeneratingHealth );

        regeneratingHealth = StartCoroutine( nameof( RegenerateHealth ) );
    }

    private void KillPlayer()
    {
        /* We are dead */
        currentHealth = 0;

        /* For our listeners, send the action for when we take damage */
        OnDamage?.Invoke( currentHealth ); 

        /* On Dead we don't regenerate health */
        if ( regeneratingHealth != null )
            StopCoroutine( regeneratingHealth );
    }

    private void HandleMouseLook()
    {
        /* Clamp our camera angles */
        rotationX -= Input.GetAxis( "Mouse Y" ) * lookSpeedY;
        rotationX = Mathf.Clamp( rotationX, -upperLookLimit, lowerLookLimit );

        /* Rotate the actual camera */
        playerCamera.transform.localRotation = Quaternion.Euler( rotationX, 0, 0 );

        /* Rotate the player*/
        // TODO: Rust like camera!
        transform.rotation *= Quaternion.Euler( 0, Input.GetAxis( "Mouse X" ) * lookSpeedX, 0 );
    }

    private void HandleViewBob()
    {
        if ( !characterController.isGrounded )
            return;

        /* Check if we are actually moving */
        if ( Mathf.Abs( moveDirection.x ) > 0.1f || Mathf.Abs( moveDirection.z ) > 0.1f  )
        {
            timer += Time.deltaTime * ( IsInternalCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed );
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin( timer ) * ( IsInternalCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount ),
                playerCamera.transform.localPosition.x
            );
        }
    }

    //TODO: Crippling statos == you can't run, limit our sprint based on our health
    private void HandleStamina()
    {
        playerAnim.SetBool( isSprintingHash, IsSprinting );

        /* Only drain our stamina if we are running and we aren't crouching */
        if ( IsSprinting && currentInput != Vector2.zero && !IsInternalCrouching )
        {
            /* Stop our coroutine if we are running */
            if ( regeneratingStamina != null )
            {
                StopCoroutine( regeneratingStamina );
                regeneratingStamina = null;
            }

            currentStamina -= staminaUseMultiplier * Time.deltaTime;

            /* Don't drain our stamina if we don't had any left! */
            if ( currentStamina < 0 )
                currentStamina = 0;

            /* Send a Notification to our listener when our stamina change */
            OnStaminaChange?.Invoke( currentStamina );

            /* Disable our running if we don't had enough stamina! */
            if ( currentStamina < 0 )
                canSprint = false;
        }

        /* If we aren't running start regenare stamina */
        if ( !IsInternalCrouching && currentStamina < maxStamina && regeneratingStamina == null )
            regeneratingStamina = StartCoroutine( nameof( RegenerateStamina ) );
    }

    private void HandleJumping()
    {
        playerAnim.SetBool( isJumpingHash, IsJumping );

        /* Jump, no needs more explanation */
        if ( IsJumping )
        {
            moveDirection.y = jumpForce;
            playerAnim.CrossFade( jumpAnimation, animationPlayTransition );
        }
    }

    private void HandleCrouching()
    {
        if ( IsCrouching )
        {
            playerAnim.CrossFade( crouchAnimation, animationPlayTransition );
            StartCoroutine( nameof( CrouchStand ) );
        }
    }

    private void HandleZoom()
    {
        if ( Input.GetKeyDown( zoomKey ) )
        {
            /* If we are in the middle of a zoom, stop us */
            if ( zoomRoutine != null )
            {
                StopCoroutine( zoomRoutine );
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine( ToggleZoom( true ) );
        }

        if ( Input.GetKeyUp( zoomKey ) )
        {
            /* If we are in the middle of a zoom, stop us */
            if ( zoomRoutine != null )
            {
                StopCoroutine( zoomRoutine );
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine( ToggleZoom( false ) );
        }
    }

    private void HandleInteractionCheck()
    {
        if ( Physics.Raycast( playerCamera.ViewportPointToRay( interactionRayPoint ), out RaycastHit hit, interactionDistance ) )
        {
            if ( hit.collider.gameObject.layer == 3 && ( currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID() ) )
            {
                hit.collider.TryGetComponent( out currentInteractable );
                if ( currentInteractable )
                    currentInteractable.OnFocus();
            }
        }
        else if ( currentInteractable )
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }

    private void HandleInteractionInput()
    {
        /* Check if we can interact with a object in the world */
        if ( Input.GetKeyDown( interactionKey ) && 
             currentInteractable != null && 
             Physics.Raycast( playerCamera.ViewportPointToRay( interactionRayPoint ), out RaycastHit hit, interactionDistance, interactionLayer ) )
        {
            currentInteractable.OnInteract();
        }
    }

    private void ApplyFinalMovement()
    {
        /* Setup our Character gravity and movement */
        if ( !characterController.isGrounded )
            moveDirection.y -= gravity * Time.deltaTime;

        /* Handles if we are on slopes or no */
        if ( canSlideOnSlopes && IsSliding )
            moveDirection += new Vector3( hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z ) * slopeSpeed;

        characterController.Move( moveDirection * Time.deltaTime );
    }

    private IEnumerator CrouchStand()
    {
        if ( IsInternalCrouching && Physics.Raycast( playerCamera.transform.position, Vector3.up, 1f ) )
            yield break;
        
        duringCrouchAnimation = true;

        float timeElapse = 0;
        float targetHeight = IsInternalCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = IsInternalCrouching ? standingCenter : crouchCenter;
        Vector3 currentCenter = characterController.center;

        while ( timeElapse < timeToCrouch )
        {
            characterController.height = Mathf.Lerp( currentHeight, targetHeight, timeElapse/timeToCrouch );
            characterController.center = Vector3.Lerp( currentCenter, targetCenter, timeElapse/timeToCrouch );

            timeElapse += Time.deltaTime;

            yield return null;
        }

        /* Sanity Check: Give us our exact crouch numbers */
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        IsInternalCrouching = !IsInternalCrouching;

        playerAnim.SetBool( isCrouchingHash, IsInternalCrouching );
        //playerAnim.CrossFade( crouchAnimation, animationPlayTransition );

        duringCrouchAnimation = false;
    }

    private IEnumerator ToggleZoom( bool isEnter )
    {
        float targeFOV = isEnter ? zoomFOV : defaultFOV;
        float startingFOV = playerCamera.fieldOfView;
        float timeElapse = 0;

        while ( timeElapse < timeToZoom )
        {
            playerCamera.fieldOfView = Mathf.Lerp( startingFOV, targeFOV, timeElapse/timeToZoom );

            timeElapse += Time.deltaTime;

            yield return null;
        }

        playerCamera.fieldOfView = targeFOV;
        zoomRoutine = null;
    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds( timeBeforeRegenStart );
        WaitForSeconds timeToWait = new WaitForSeconds( healthTimeIncrement );

        while ( currentHealth < maxHealth )
        {
            currentHealth += healthValueIncrement;

            if ( currentHealth > maxHealth )
                currentHealth = maxHealth;

            /* For our listeners, send the action for when we regenerate health */
            OnHeal?.Invoke( currentHealth ); 

            yield return timeToWait;
        }

        regeneratingHealth = null;
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds( timeBeforeStaminaRegenStart );
        WaitForSeconds timeToWait = new WaitForSeconds( staminaTimeIncrement );

        while ( currentStamina < maxStamina )
        {
            if ( currentStamina > 0 )
                canSprint = true;

            currentStamina += staminaValueIncrement;

            if ( currentStamina > maxStamina )
                currentStamina = maxStamina;

            /* For our listeners, send the action for when we regenerate health */
            OnStaminaChange?.Invoke( currentStamina ); 

            yield return timeToWait;
        }

        regeneratingStamina = null;
    }
}