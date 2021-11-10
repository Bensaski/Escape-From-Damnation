using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    Transform Player;
    GameObject Player1;
    public float Velocity = 30f;
    public int speed = 300;
    int Distance = 1;
    int waitingTime = 1;
    public GameObject projectile;
    bool inRange = false;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        health = 10;
        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player = GameObject.FindGameObjectWithTag("Player1").transform;

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Vector3.Distance(transform.position, Player.position) <= 10)
        {
            inRange = true;
        }
        if (inRange) {
            transform.LookAt(Player);

            if (Vector3.Distance(transform.position, Player.position) >= Distance)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            if (timer > waitingTime)
            {
                ShootPlayer();
            }
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

    private void ShootPlayer()
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
        //bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, Velocity, 0));
        bullet.GetComponent<Rigidbody>().velocity = (Player1.transform.position - bullet.transform.position).normalized * Velocity;
        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * Velocity);
        timer = 0;
        Destroy(bullet, 3f);

    }

}
