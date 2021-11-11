using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gate : MonoBehaviour
{
    float count;
    GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
       enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            count++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        count = 0;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
            {
                count++;
            }
        }

        if(count == 0)
        {
            Destroy(gameObject);
        }
    }
}
