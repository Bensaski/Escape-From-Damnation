using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{

    public int difficulty = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Easy()
    {
        difficulty = 1;
        PlayerPrefs.SetInt("difficulty", 1);
    }

    public void Hard()
    { 
        difficulty = 2;
        PlayerPrefs.SetInt("difficulty", 2);
    }
}
