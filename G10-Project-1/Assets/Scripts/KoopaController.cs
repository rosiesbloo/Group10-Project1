using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaController : MonoBehaviour
{
    public float moveX;
    public float moveSpeed;
    private Rigidbody rb;
    public GameObject Shell;
    public Transform Spawnpoint;
    public GameObject Turtle;



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
            Instantiate(Shell, Spawnpoint.position, Spawnpoint.rotation);
            
           Destroy(gameObject);
            
            ScoreCounter.score += +200;
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
            ScoreCounter.score += +200;
            Destroy(gameObject, 0f);
        }

       
        
    }
}
