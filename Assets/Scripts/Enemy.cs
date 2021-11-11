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
    int Distance = 10;
    int waitingTime = 2;
    public GameObject projectile;
    bool inRange = false;
    float timer;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player = GameObject.FindGameObjectWithTag("Player1").transform;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (Vector3.Distance(transform.position, Player.position) <= 10)
        {
            inRange = true;
        }
        if (inRange) {
            transform.LookAt(Player);

            if (Vector3.Distance(transform.position, Player.position) >= 3)
            {
                rb.AddForce(transform.forward * speed);
               //transform.position += transform.forward * speed * Time.deltaTime;
            }
            if (Vector3.Distance(transform.position, Player.position) < 3)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                //transform.position += transform.forward * speed * Time.deltaTime;
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
