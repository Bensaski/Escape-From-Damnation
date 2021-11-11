using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class win : MonoBehaviour
{
    GameControllerScript gs;
    public GameObject player;
    // Start is called before the first frame update

    void Start()
    {
        gs = player.GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            gs.winText.text = "You Win!";
            //Time.timeScale = 0;
            Invoke("MainMenu", 3);
        }
    }
    void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
