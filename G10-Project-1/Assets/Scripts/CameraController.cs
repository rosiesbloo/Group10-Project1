using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothness = 15f;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetCameraPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothness * Time.deltaTime);
    }
}
