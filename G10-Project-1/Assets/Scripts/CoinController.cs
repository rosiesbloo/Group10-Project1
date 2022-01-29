using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
            {

            CoinCounter.coin += 1;
            ScoreCounter.score += 200;

            Destroy(this.gameObject);
            }

    }
}
