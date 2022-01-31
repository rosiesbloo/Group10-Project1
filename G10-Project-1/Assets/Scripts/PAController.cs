using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PAController : MonoBehaviour
{
    public void StartAgain()
    {
        SceneManager.LoadScene("PlayerSelect");
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

}
