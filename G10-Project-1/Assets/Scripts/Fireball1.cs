using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball1 : MonoBehaviour
{
  public float moveX;
    public float moveSpeed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveX = 2f;
        moveSpeed = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(moveX * moveSpeed, rb.velocity.y);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Enemy Body"))
        {
            Destroy(gameObject,0.01f);
        }
    }

}
