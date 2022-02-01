using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseScreen;
    public AudioSource pauseSound;

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause))
        {

            if (isPaused)
            {
                Continue();
            }
            else
            {
                pauseSound.Play();
                Pause();
            }

        }
        
    }

    public void Continue()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

}
