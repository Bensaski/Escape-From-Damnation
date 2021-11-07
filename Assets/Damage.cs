using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("hi");
            Enemy Enemy = other.gameObject.GetComponent<Enemy>();

            Enemy.gameObject.SendMessage("recieveDamage", 1);
            Destroy(gameObject);
        }
    }
}
