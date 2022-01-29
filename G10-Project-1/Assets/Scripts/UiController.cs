using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    //Score
    public Text scoreText;
    public Text coinText;
    public Text timerText;
    private int score;
    private int coinCount;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        coinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        coinText.text = coinCount.ToString();
    }
}
