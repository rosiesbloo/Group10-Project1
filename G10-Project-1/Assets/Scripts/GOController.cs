using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GOController : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartOver()
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
