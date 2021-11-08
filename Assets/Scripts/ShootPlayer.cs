using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    float Velocity = 10f;
    public GameObject Enemy;
    float timer;
    int waitingTime = 3;
    Transform playerT;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player1");

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            
            GameObject bullet = Instantiate(projectile, Enemy.transform.position, Enemy.transform.rotation);
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
            //bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, Velocity, 0));
            bullet.GetComponent<Rigidbody>().velocity = (player.transform.position - bullet.transform.position).normalized * Velocity;
            //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * Velocity);
            timer = 0;
            Destroy(bullet, 3f);
            
            
        }




        }
    }
    



