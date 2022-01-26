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
        moveX = -1f;
        moveSpeed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(moveX * moveSpeed, rb.velocity.y);
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
