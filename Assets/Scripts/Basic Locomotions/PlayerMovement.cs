using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    #region SingleTon
    public static PlayerMovement Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    #endregion
    [Header("Player Settings")]
    [SerializeField]VariableJoystick joystick;
    float Speed=5f;
    public float WalkSpeed;
    public float SprintSpeed = 10f;


    [Header("Objective Settings")]
    [SerializeField] int ObjectiveReciver;

    [Header("Crouch Settings")]
    public float CrouchHeight;
    public float CrouchSpeed;

    [Header("Audio Settings")]
    public AudioClip FootStep;

    [Header("Layers")]
    public LayerMask WhatIsGround;
  
    [Header("Head Bob Settings")]
    float SprintAmlitude = 0.2f;
    float BobFrequency = 7f;

    [Header("Transforms")]
    public Transform GroundCheck;
    [SerializeField] Transform HeadCheck;

    //Character Components
    [SerializeField] HeadBob Bob;
    [SerializeField] CharacterController CC;

    //Inputs
    float Horizontal;
    float Vertical;

    //Gravity
    float Gravity = -9.81f;

    //bools
    public static bool IsWalking;
    bool isGrounded;
    bool CanCrouch;
    bool IsCrouched;
    bool IsSprinting;
    bool CanStand;

    //Vectors
    Vector3 Move;
    Vector3 Velocity;





    void Start()
    {
        //Initializing
  
        IsCrouched = false;
        IsSprinting = false;
        //Changing strings to hases
    
      
    }


    void Update()
    {
        #region Player Movement
        //GroundCheck
        isGrounded = Physics.CheckSphere(GroundCheck.position, 0.3f, WhatIsGround);
        if (Physics.CheckSphere(HeadCheck.position, 0.5f, WhatIsGround))
        {
            CanStand = false;
        }
        else CanStand = true;


        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }
        if (isGrounded) CanCrouch = true; else CanCrouch = false;


        //Getting Inputs
        Horizontal = joystick.Horizontal;
        Vertical = joystick.Vertical;
        //Horizontal=Input.GetAxis("Horizontal");
        //Vertical =Input.GetAxis("Vertical");
        //Move Vector
        Move = transform.forward * Vertical + transform.right * Horizontal;
        #endregion

        #region CCMovement
        CC.Move(Move.normalized * Speed * Time.deltaTime);
        //CcMovement
        Velocity.y += Gravity * Time.deltaTime;

        CC.Move(Velocity * Time.deltaTime);

        #endregion

        #region Walk Bool
        if ((Horizontal != 0 || Vertical != 0))
        {
            IsWalking = true;
        }
        else IsWalking = false;
        #endregion

        

    }
    
    public void Sprint()
    {
        if (IsWalking)
        {
            Speed = SprintSpeed;
            Bob.bobFrequency = BobFrequency;
            Bob.bobamplitude = SprintAmlitude;
            IsSprinting = true;
        }
        else 
        {
            Speed = WalkSpeed;
            Bob.bobFrequency = 5f;
            Bob.bobamplitude = 0.1f;
            IsSprinting = false;
        }
    }



    public void Crouch()
    {
        IsCrouched = !IsCrouched;
        if(IsCrouched)
        {
            Speed = CrouchSpeed;
            transform.localScale = new Vector3(1, CrouchHeight, 1);
            IsCrouched = true;
        }
        if(!IsCrouched && CanStand)
        {
            Speed = WalkSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            IsCrouched = false; 
        }
    }
}
