using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    
    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(target.position.x, transform.position.y, -10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(target.transform.position.x > transform.position.x)
        transform.position = new Vector3(target.position.x, transform.position.y, -10);
    }
}
