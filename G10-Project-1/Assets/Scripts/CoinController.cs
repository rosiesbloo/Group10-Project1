using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioSource coinCollectSound;

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
            {
            coinCollectSound.Play();
            CoinCounter.coin += 1;
            ScoreCounter.score += 200;
            Destroy(this.gameObject);
            }
    }
}
