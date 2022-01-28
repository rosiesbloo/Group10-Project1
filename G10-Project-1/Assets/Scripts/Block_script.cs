using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_script : MonoBehaviour
{


    [SerializeField] private Animator myAnimatorController;
 
 private void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
                       myAnimatorController.SetBool("IsHit", true);
    }

    }

private void OnTriggerExit(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
            myAnimatorController.SetBool("IsHit", false);
    }

    }


}








