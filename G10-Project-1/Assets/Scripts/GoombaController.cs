using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    public float moveX;
    public float moveSpeed;
    private Rigidbody rb;
    public AudioSource goomaHitSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveX = 0f;
        moveSpeed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(moveX * moveSpeed, rb.velocity.y);
    }

    void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Zone"))
        {
        moveX = -1f;
        moveSpeed =2f;
        }
        if (other.CompareTag("Wall"))
        {
             moveX *= -1f;
    }

        if (other.CompareTag("Player"))
        {
            MarioController controller = other.GetComponent<MarioController>();
            
            if (controller != null)
            {
          
            }
            goomaHitSound.Play();
            gameObject.tag = "Enemy Head";
            rb.isKinematic = true;
            rb.detectCollisions = false;
            moveX = 0f;
            gameObject.transform.localScale += new Vector3(0, -.5f, 0);
            ScoreCounter.score += +100;
            Destroy(gameObject, 1f);
        }

    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            moveX *= -1f;
        }
        else if (collision.gameObject.tag == "Enemy Body")
        {
            moveX *= -1f;
        }

        if(collision.gameObject.tag == "Pipe")
    {
        rb.detectCollisions = false;
        Destroy(gameObject, 0.5f);
    }

        if(collision.gameObject.tag == "Fireball")
        {
            goomaHitSound.Play();
            ScoreCounter.score += +100;
            Destroy(gameObject, .1f);
        }
          if(collision.gameObject.tag == "Shell")
        {
            goomaHitSound.Play();
            ScoreCounter.score += +100;
            Destroy(gameObject, .1f);
        }
        if (collision.gameObject.tag == "Player")
        {
            moveX *= 0f;
            rb.detectCollisions = false;
            rb.isKinematic = true;
        }

        
    }
}
