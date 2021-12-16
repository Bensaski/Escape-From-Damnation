using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
