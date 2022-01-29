using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    //Movement
    public float marioSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
    public float gravity;
    public float gravityMultiplier;


    //Animations
    public Animator animator;
    private string currentState;

    //Ground Check
    public bool isJumping;
    public bool isGrounded;
    public float groundCheckDistance;
    public LayerMask groundMask;

    //Vector3
    private Vector3 moveDirection;
    private Vector3 velocity;

    //Get Component
    private CharacterController controller;

    //Player States
    public static int MarioState;
    public Color FPColor;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        MarioState = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        isJumping = velocity.y > 0;

        float gravity = Physics.gravity.y * gravityMultiplier;

        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, 0);

        //Flip Animation
        if (Input.GetAxis("Horizontal") > 0.01f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }  
        else if (Input.GetAxis("Horizontal") < -0.01f)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
            
        //Grounded

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded)
        {
            gravityMultiplier = 4;

            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && !Input.GetButtonUp("Jump"))
            {
                marioSpeed = walkSpeed;
                ChangeAnimationState("Little_Mario_Walk");
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !Input.GetButtonUp("Jump"))
            {
                marioSpeed = runSpeed;
                ChangeAnimationState("Little_Mario_Run");
            }
            else if (moveDirection == Vector3.zero && !Input.GetButton("Vertical") && !Input.GetButtonUp("Jump"))
            {
                marioSpeed = 3;
                ChangeAnimationState("Little_Mario_Idle");
            }
            else if(moveDirection == Vector3.zero && Input.GetButton("Vertical"))
            {
                ChangeAnimationState("Little_Mario_Crouch");
            }

            if (Input.GetButtonDown("Jump") == true)
            {
                isJumping = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -4 * gravity);
                ChangeAnimationState("Little_Mario_Jump");
            }
        }


        //Jumping

        if (isJumping && velocity.y > 0 && Input.GetButtonUp("Jump"))
        {
            gravityMultiplier = 10;
        }

        //Escape Key

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        //Move

        moveDirection *= marioSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //States

        MarioStates();

    }

    void ChangeAnimationState(string newState)
      {
        if(currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
     }

    void MarioStates()
    {
        if(MarioState == 0)
        {
            transform.localScale = new Vector3(1.1f, 0.98f, 0f);
            groundCheckDistance = 0.58001f;
        }

        if(MarioState == 1)
        {
            transform.localScale = new Vector3(1.6f, 1.9f, 0);
            groundCheckDistance = 0.95f;
        }

        if(MarioState == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = FPColor;
            transform.localScale = new Vector3(1.6f, 1.9f, 0);
            groundCheckDistance = 0.95f;
        }
    }

}
