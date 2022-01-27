using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_script : MonoBehaviour
{

 public Animator anim;

 void Start ()
 {
     anim = GetComponent<Animator>();
 }

 private void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
     anim.Play("CubeBounce2");
    }

    }


}








