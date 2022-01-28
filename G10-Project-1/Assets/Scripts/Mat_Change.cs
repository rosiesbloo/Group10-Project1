using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mat_Change : MonoBehaviour
{
    public string objectName;
    public Material[] material;
    Renderer rend;
    
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
            GameObject someGameOnject = GameObject.Find(objectName);
            rend.sharedMaterial = material[1];
        }
    }
}
