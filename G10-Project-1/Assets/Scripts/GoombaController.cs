using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaController : MonoBehaviour
{
    public float moveX;
    public float moveSpeed;
    private Rigidbody rb;



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

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Zone"))
        {
        moveX = -1f;
        moveSpeed =2f;
        }

        if (other.CompareTag("Player"))
        {
            MarioController controller = other.GetComponent<MarioController>();
            
            if (controller != null)
            {
          
            }

            ScoreCounter.score += +100;
            Destroy(gameObject, 0f);
        }

    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pipe")
        {
            moveX *= -1f;
        }
        else if (collision.gameObject.tag == "Goomba")
        {
            moveX *= -1f;
        }
    }
}
