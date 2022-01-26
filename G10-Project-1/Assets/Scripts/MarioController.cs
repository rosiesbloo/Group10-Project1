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


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        isJumping = velocity.y > 0;

        float gravity = Physics.gravity.y * gravityMultiplier;

        float moveX = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(moveX, 0, 0);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded)
        {
            gravityMultiplier = 4;

            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                marioSpeed = walkSpeed;
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                marioSpeed = runSpeed;
            }
            else if (moveDirection == Vector3.zero)
            {
                marioSpeed = 3;
            }

            if (Input.GetButtonDown("Jump") == true)
            {
                isJumping = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -4 * gravity);
            }
        }

        if (isJumping && velocity.y > 0 && Input.GetButtonUp("Jump"))
        {
            gravityMultiplier = 10;
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        moveDirection *= marioSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
