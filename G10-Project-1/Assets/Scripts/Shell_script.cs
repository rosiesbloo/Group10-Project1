using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_script : MonoBehaviour
{
   public float moveX;
    public float moveSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveX = 1f;
        moveSpeed = 8f;
       
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(moveX * moveSpeed, rb.velocity.y);
        
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))
        {
        rb.isKinematic = true;
        moveSpeed = 0f;
        }
    }
    void OnTriggerExit(Collider other)
    {
         if (other.CompareTag("Player"))
        {
        rb.isKinematic = false;
        moveX = -1f;
        moveSpeed = -10f;
        }
    }
    
     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            moveX *= -1f;
        }
        
        if(collision.gameObject.tag == "Pipe")
    {
        rb.detectCollisions = false;
        Destroy(gameObject, 0.5f);
    }

        if(collision.gameObject.tag == "Player")
{
        GetComponent<Collider>().isTrigger = true;
}
              
    }

}
