using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && MarioController.MarioState == 1)
        {
            MarioController.MarioState = 2;

            ScoreCounter.score += +1000;
            Destroy(this.gameObject);
        }
    }
}
