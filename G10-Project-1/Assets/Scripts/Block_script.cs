using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_script : MonoBehaviour
{
    public Material Mat;
    public bool hasCoin;
    public bool isQuestion;
    public bool isbrick;
    public bool hasObject;

    public static int coinCount;

    public Transform Spawnpoint;
    public GameObject Prefab1;
    public GameObject Prefab2;
    private MarioController marioController;
        public ParticleSystem particleBurst;
        public ParticleSystem coinBurst;
    [SerializeField] private Animator myAnimatorController;

    void start ()
    {
        coinCount = 0;
          }
 
 private void OnTriggerEnter(Collider other)
    {
 
        if (other.CompareTag("Player"))
        {
    if(isbrick == false && isQuestion == true)
            {
             if(hasCoin == true && hasObject == false)
            {
                coinBurst.Play();
                ScoreCounter.score += +200;
                
                hasCoin = false;

            
            if(MarioController.MarioState == 0 || MarioController.MarioState == 1 || MarioController.MarioState == 2)
            {
                
                       myAnimatorController.SetBool("IsHit", true);
                    Destroy(gameObject,0.25f);
            }
            
            }

             if(hasCoin == false && hasObject == true)
            {
            if(MarioController.MarioState == 0 || MarioController.MarioState == 1 || MarioController.MarioState == 2)
            {
               
                      
                       Destroy(gameObject,0.25f);
            }
            if(MarioController.MarioState == 0)
            {
                 Instantiate(Prefab1, Spawnpoint.position, Spawnpoint.rotation);
            }
            if(MarioController.MarioState == 1 || MarioController.MarioState == 2)
            {
                 Instantiate(Prefab2, Spawnpoint.position, Spawnpoint.rotation);
            }
            }

            }
            

 if(isbrick == true && isQuestion == false)
            {
            if(hasCoin == true && hasObject == false)
            {
                coinBurst.Play();
                coinCount += 1;
             ScoreCounter.score += +200;

            
            if(MarioController.MarioState == 0 || MarioController.MarioState == 1 || MarioController.MarioState == 2)
            {
                       myAnimatorController.SetBool("IsHit", true);
            }
            if(coinCount == 10)
            {
                       
                       transform.parent.GetComponent<Renderer>().material = Mat;
                        Destroy(gameObject,0.25f);
            }
            }

             if(hasCoin == false && hasObject == false)
            {
            if(MarioController.MarioState == 0)
            {
                       myAnimatorController.SetBool("IsHit", true);
            }
            if(MarioController.MarioState == 1 || MarioController.MarioState == 2)
            {
                       particleBurst.Play();
                       ScoreCounter.score += +50;
                        Destroy(transform.parent.gameObject,0.25f);
            }
            }
            if(hasCoin == false && hasObject == true)
            {
            
            if(MarioController.MarioState == 0 || MarioController.MarioState == 1 || MarioController.MarioState == 2)
            {
                       Instantiate(Prefab1, Spawnpoint.position, Spawnpoint.rotation);
                       transform.parent.GetComponent<Renderer>().material = Mat;
                        Destroy(gameObject,0);
                       
            }
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








