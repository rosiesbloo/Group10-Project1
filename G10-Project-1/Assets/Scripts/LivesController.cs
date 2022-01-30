using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesController : MonoBehaviour
{
    public GameObject livesCount;
    public static int maxLives = 4;
    public static int currentLives = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        livesCount.GetComponent<Text>().text = "" + currentLives;
    }
}
