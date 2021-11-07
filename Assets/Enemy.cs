using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public Transform Player;
    GameObject Player1;
    public int speed = 300;
    int Distance = 1;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if(Vector3.Distance(transform.position,Player.position) >= Distance)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    void recieveDamage(int damage)
    {
        health = health - damage;
        if(health <= 0)
        {
            
        
            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trig");
        if (other.CompareTag("Player1"))
        {
            other.gameObject.SendMessage("recieveDamage", 1);
        }
    }

}
