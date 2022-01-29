using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_script : MonoBehaviour
{
    public bool hasCoin;
    private MarioController marioController;
        public ParticleSystem particleBurst;
        public ParticleSystem coinBurst;
    [SerializeField] private Animator myAnimatorController;

    
 
 private void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
            if(hasCoin == true)
            {
                coinBurst.Play();
                
                hasCoin = false;

            
            if(MarioController.MarioState == 0)
            {
                       myAnimatorController.SetBool("IsHit", true);
            }
            if(MarioController.MarioState == 1)
            {
                       particleBurst.Play();
                        Destroy(transform.parent.gameObject,0.25f);
            }
            }

             if(hasCoin == false)
            {
            if(MarioController.MarioState == 0)
            {
                       myAnimatorController.SetBool("IsHit", true);
            }
            if(MarioController.MarioState == 1)
            {
                       particleBurst.Play();
                        Destroy(transform.parent.gameObject,0.25f);
            }
            }

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








