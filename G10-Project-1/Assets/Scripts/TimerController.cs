using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public float currentTime;
    public float startingTime = 160;
    public Text countdown;

    public void Start()
    {
        currentTime = startingTime;
    }

    public void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdown.text = currentTime.ToString("0");
    
        if(currentTime <= 1)
        {
            currentTime = 0;
            Invoke(nameof(ReloadScene), 1.5f);
        }
    
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
