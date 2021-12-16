using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{

    // Start is called before the first frame update
    public void Switch()
    {
        PlayerPrefs.SetInt("Shots", 1);
        SceneManager.LoadScene("LevelOne");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Quit()
    {
        Application.Quit();
    }
}

