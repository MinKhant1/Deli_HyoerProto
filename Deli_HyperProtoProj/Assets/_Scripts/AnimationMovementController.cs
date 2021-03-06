using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimationMovementController : MonoBehaviour
{

   PI playerInput;
   CharacterController characterController;
   public Animator PlayerAnimator;


    Vector2 currentMovementInput;
    Vector3 currentMovement;
    bool isMovementPressed;

    [SerializeField]
    float rotationFactorPerFrame;
    
   public float playerSpeed;


    int isRunningHash;

   

    private void Awake()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        playerInput = new PI();
        characterController = GetComponent<CharacterController>();
       

        isRunningHash = Animator.StringToHash("IsRunning");

        playerInput.CharacterMovement.Move.started += onMovementInput;
        playerInput.CharacterMovement.Move.canceled += onMovementInput;
        playerInput.CharacterMovement.Move.performed += onMovementInput;
    }



    void onMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleAnimation();
        handleRotation();
        handleGravity();
        characterController.Move(currentMovement*Time.deltaTime*playerSpeed);


    }

    void handleGravity()
    {
        if(characterController.isGrounded)
        {
            float groundedGravity = -0.5f;
            currentMovement.y = groundedGravity;
        }
        else
        {
            float gravity = -2f;
            currentMovement.y+=gravity; 
           
        }
    }

    void handleAnimation()
    {
        bool isRunning = PlayerAnimator.GetBool("IsRunning");
        if(isMovementPressed && !isRunning)
        {
            PlayerAnimator.SetBool(isRunningHash, true);
        }
        else if(!isMovementPressed && isRunning)
        {
            PlayerAnimator.SetBool(isRunningHash, false);
        }
    }
    void handleRotation()
    {
        Vector3 positionToLookAt;
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        Quaternion currentRotation = transform.rotation;
        if(isMovementPressed)
        {
         Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame*Time.deltaTime);

        }
    }

    private void OnEnable()
    {
        playerInput.CharacterMovement.Enable();
    }
    private void OnDisable()
    {
        playerInput.CharacterMovement.Disable();
    }

   
}
