using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MarioController : MonoBehaviour
{
    //Movement
    public float marioSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float loseSpeed = 0f;
    public float jumpHeight;
    public float gravity;
    public float gravityMultiplier;
    public float timeInvincible = 0.5f;
    public float timeStarman = 10f;
    bool isInvincible;
    bool isStarman;
    float invincibleTimer;
    float starmanTimer;
    public Transform Spawnpoint1;
     public Transform Spawnpoint2;
    public GameObject Fireball1;
    public GameObject Fireball2;

    
    
    public Transform teleportTarget1;
    public Transform teleportTarget2;

    //Camera
    public Camera camera1;
    public Camera camera2;

    //Underground
    public bool Underground;
    public bool Tube;

    //Music and Sound Effects 
    public AudioSource mainMusic;
    public AudioSource hurryMusic;
    public AudioSource winMusic;
    public AudioSource dieMusic;
    public AudioSource jumpSound;
    public AudioSource starmanMusic;
    public AudioSource enterPipe;

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
    Collider collider;

    //Player States
    public static int MarioState;
    public Color FPColor;
    public Color normalColor;




    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        MarioState = 0;
        Tube = false;
        Underground = false;
              
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

        if (isInvincible)
        {
        
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }        
        }

        if (isStarman)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = FPColor;
            starmanTimer -= Time.deltaTime;
            if (starmanTimer < 0)
            {
                isStarman = false;
                starmanMusic.Stop();
                mainMusic.Play();
                gameObject.GetComponent<SpriteRenderer>().material.color = normalColor;
            }
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

                if (MarioState == 0 || MarioState == 1)
                {
                    ChangeAnimationState("Little_Mario_Walk");
                }
                else if (MarioState == 2)
                {
                    ChangeAnimationState("Fire_Mario_Walk");
                }
                else if (isStarman)
                {
                    ChangeAnimationState("Starman_Run");
                }
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !Input.GetButtonUp("Jump") || moveDirection != Vector3.zero && Input.GetKey(KeyCode.Z) && !Input.GetButtonUp("Jump") || moveDirection != Vector3.zero && Input.GetKey(KeyCode.RightShift) && !Input.GetButtonUp("Jump"))
            {
                marioSpeed = runSpeed;
                if (MarioState == 0 || MarioState == 1)
                {
                    ChangeAnimationState("Little_Mario_Run");
                }
                else if (MarioState == 2)
                {
                    ChangeAnimationState("Fire_Mario_Run");
                }
                else if (isStarman)
                {
                    ChangeAnimationState("Starman_Run");
                }
            }
            else if (moveDirection == Vector3.zero && !Input.GetButton("Vertical") && !Input.GetButtonUp("Jump"))
            {
                marioSpeed = 3;
                if (MarioState == 0 || MarioState == 1)
                {
                    ChangeAnimationState("Little_Mario_Idle");
                }
                else if (MarioState == 2)
                {
                    ChangeAnimationState("Fire_Mario_Idle");
                }
                else if (isStarman)
                {
                    ChangeAnimationState("Starman_Idle");
                }
            }
            else if (moveDirection == Vector3.zero && Input.GetButton("Vertical"))
            {

                if (MarioState == 0 || MarioState == 1)
                {
                    
                    ChangeAnimationState("Little_Mario_Crouch");
                }
                else if (MarioState == 2)
                {
                    
                    ChangeAnimationState("Fire_Mario_Crouch");
                }
                else if (isStarman)
                {
                    ChangeAnimationState("Starman_Crouch");
                }
            }

            if (Input.GetButtonDown("Jump") == true)
            {
                jumpSound.Play();
                isJumping = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -4 * gravity);
                if (MarioState == 0 || MarioState == 1)
                {
                    ChangeAnimationState("Little_Mario_Jump");
                }
                else if (MarioState == 2)
                {
                    ChangeAnimationState("Fire_Mario_Jump");
                }
                else if (isStarman)
                {
                    ChangeAnimationState("Starman_Jump");
                }
            }

            if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
               if(Tube == true)
               {
                   enterPipe.Play();
                   Underground = true;
                   transform.position = teleportTarget1.transform.position;
               }
            }

            if(Underground == true)
            {
              camera1.enabled = false;
              camera2.enabled = true;
            }
               if(Underground == false)
            {
              camera1.enabled = true;
              camera2.enabled = false;
            }
           
        }


        //Jumping

        if (isJumping && velocity.y > 0 && Input.GetButtonUp("Jump"))
        {
            gravityMultiplier = 15;
        }



        //Escape Key

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        //Fireball

        if(Input.GetKeyDown(KeyCode.LeftShift) && MarioState == 2 || Input.GetKeyDown(KeyCode.Z) && MarioState == 2 || Input.GetKeyDown(KeyCode.RightShift) && MarioState == 2)
        {
            if( gameObject.GetComponent<SpriteRenderer>().flipX == false)
            {
            ChangeAnimationState("Fire_Mario_Throw");
            Instantiate(Fireball1, Spawnpoint1.position, Spawnpoint1.rotation);
            }

            if( gameObject.GetComponent<SpriteRenderer>().flipX == true)
            {
            ChangeAnimationState("Fire_Mario_Throw");
            Instantiate(Fireball2, Spawnpoint2.position, Spawnpoint2.rotation);
            }
        }

        //Move

        moveDirection *= marioSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //States

        MarioStates();

    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            Die();
            LivesController.currentLives -= 1;
            ChangeAnimationState("Little_Mario_Lose");

            if (isInvincible)
                return;
        }

        if(other.gameObject.CompareTag("UGD"))
            {
                Tube = true;
            }


        if(other.gameObject.CompareTag("UGU"))
            {
              enterPipe.Play();
              Underground = false;
              Tube = false;
              transform.position = teleportTarget2.transform.position;
            }

        if(other.gameObject.CompareTag("Star"))
            {
            starmanMusic.Play();
            mainMusic.Stop();
            hurryMusic.Stop();
            isStarman = true;
            starmanTimer = timeStarman;
            }

        if (other.gameObject.CompareTag("Enemy Body") || other.gameObject.CompareTag("Shell"))
        {
            if(isStarman == false)
            {
            if (isInvincible == false)
            {
                if (MarioState == 0)
                {
                    collider.enabled = false;
                    Die();
                    marioSpeed = 0f;
                    walkSpeed = 0f;
                    runSpeed = 0f;
                    jumpHeight = 0f;
                    LivesController.currentLives -= 1;
                    ChangeAnimationState("Little_Mario_Lose");

                    if (isInvincible)
                        return;
                }
                if (MarioState == 1)
                {
                    MarioController.MarioState = 0;

                    isInvincible = true;
                    invincibleTimer = timeInvincible;


                }
                if (MarioState == 2)
                {
                    MarioController.MarioState = 0;
                    isInvincible = true;
                    invincibleTimer = timeInvincible;

                }

            }
         }

         if (isStarman == true)
            {
                ScoreCounter.score += 100;
                Destroy(GameObject.FindWithTag("Enemy Body"));
            }
        }
        
    }
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("UGD"))
            {
                Tube = false;
            }
    }

    void OnTriggerEnter(Collider other)
    {
       

        if(other.gameObject.tag == "ZoneBreak")
        {
            Destroy(GameObject.FindWithTag("Zone"));
        }

        if (other.gameObject.tag == "FlagPole")
        {
            winMusic.Play();
            mainMusic.Stop();
            walkSpeed = 0f;
            runSpeed = 0f;
            marioSpeed = 0f;
            jumpHeight = 0f;

            if(MarioState == 0 || MarioState == 1)
            {
                ChangeAnimationState("Little_Mario_Win");
            }
            
            if(MarioState == 2)
            {
                ChangeAnimationState("Fire_Mario_Win");
            }
        }

        if (other.gameObject.tag == "FlagBase")
        {
            Destroy(GameObject.FindWithTag("FlagPole"));
            if(MarioState == 0 || MarioState == 1)
            {
                ChangeAnimationState("Little_Mario_Idle");
            }
            if(MarioState == 2)
            {
                ChangeAnimationState("Fire_Mario_Idle");
            }

            walkSpeed = 4f;
            runSpeed = 4f;
            marioSpeed = 4;
            jumpHeight = 2.5f;
        }

        if (other.gameObject.tag == "FiveTPoints")
        {

            ScoreCounter.score += 2000;
            Destroy(GameObject.FindWithTag("EightPounts"));
            Destroy(GameObject.FindWithTag("FiveTPoints"));
            Destroy(GameObject.FindWithTag("TwoTPoints"));
            Destroy(GameObject.FindWithTag("FourPoints"));
            Destroy(GameObject.FindWithTag("OnePoint"));
        }

        if (other.gameObject.tag == "TwoTPoints")
        {
            ScoreCounter.score += 5000;
            Destroy(GameObject.FindWithTag("EightPounts"));
            Destroy(GameObject.FindWithTag("FiveTPoints"));
            Destroy(GameObject.FindWithTag("TwoTPoints"));
            Destroy(GameObject.FindWithTag("FourPoints"));
            Destroy(GameObject.FindWithTag("OnePoint"));
        }

        if (other.gameObject.tag == "EightPounts")
        {
            ScoreCounter.score += 800;
            Destroy(GameObject.FindWithTag("EightPounts"));
            Destroy(GameObject.FindWithTag("FiveTPoints"));
            Destroy(GameObject.FindWithTag("TwoTPoints"));
            Destroy(GameObject.FindWithTag("FourPoints"));
            Destroy(GameObject.FindWithTag("OnePoint"));
        }

        if (other.gameObject.tag == "FourPoints")
        {
            ScoreCounter.score += 400;
            Destroy(GameObject.FindWithTag("EightPounts"));
            Destroy(GameObject.FindWithTag("FiveTPoints"));
            Destroy(GameObject.FindWithTag("TwoTPoints"));
            Destroy(GameObject.FindWithTag("FourPoints"));
            Destroy(GameObject.FindWithTag("OnePoint"));
        }

        if (other.gameObject.tag == "OnePoint")
        {
            ScoreCounter.score += 100;
            Destroy(GameObject.FindWithTag("EightPounts"));
            Destroy(GameObject.FindWithTag("FiveTPoints"));
            Destroy(GameObject.FindWithTag("TwoTPoints"));
            Destroy(GameObject.FindWithTag("FourPoints"));
            Destroy(GameObject.FindWithTag("OnePoint"));
        }

            if (other.gameObject.tag == "Winner")
        {
            WinGame();
        }

    }

    void MarioStates()
    {
        if (MarioState == 0)
        {
            transform.localScale = new Vector3(1.1f, 0.98f, 0f);
            groundCheckDistance = 0.58f;
        }

        if (MarioState == 1)
        {
            transform.localScale = new Vector3(1.4f, 1.8f, 0);
            groundCheckDistance = 0.95f;
        }

        if (MarioState == 2)
        {
            transform.localScale = new Vector3(1.4f, 1.8f, 0);
            groundCheckDistance = 0.95f;
        }

    }

    void WinGame()
    {
        Invoke(nameof(YouWin), 0.5f);
    }

    public void Die()
    {
        dieMusic.Play();
        mainMusic.Stop();
        if (LivesController.currentLives <= 1)
        {
            Invoke(nameof(GameOver), 2.6f);
        }
        else
            Invoke(nameof(ReloadScene), 2.6f);
    }

    public void GameOver()
    {
        LivesController.currentLives = 3;
        ScoreCounter.score = 0;
        CoinCounter.coin = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void YouWin()
    {
        LivesController.currentLives = 3;
        ScoreCounter.score = 0;
        CoinCounter.coin = 0;
        SceneManager.LoadScene("PlayAgain");
    }

}
