using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public int health;
    Transform Player;
    GameObject Player1;
    GameObject PlayerTarget;
    public float Velocity = 30f;
    public int speed = 300;
    int Distance = 10;
    public int waitingTime = 2;
    public GameObject projectile;
    bool inRange = false;
    float timer;
    Rigidbody rb;
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    public GameObject bulletSpawner;

    // Start is called before the first frame update
    void Start()
    {

        Player1 = GameObject.FindGameObjectWithTag("Player1");
        Player = GameObject.FindGameObjectWithTag("Player1").transform;
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        nextDestination();


        if (PlayerPrefs.GetInt("difficulty") == 1) //easy 
        {
            health = 15;
        }
        if (PlayerPrefs.GetInt("difficulty") == 2) //hard
        {
            health = 30;
        }
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
            agent.SetDestination(Player.transform.position);
            transform.LookAt(Player);

            if (timer > waitingTime)
            {
                ShootPlayer();
            }
            if(agent.remainingDistance < 3f)
            {
                agent.isStopped = true;
             
            }
            else
            {
                agent.isStopped = false;
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

    void nextDestination()
    {
        if (points.Length == 0)
        {
            return;
        }
        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;

    }



    private void ShootPlayer()
    {
        GameObject bullet = Instantiate(projectile, bulletSpawner.transform.position, transform.rotation);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
        //bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, Velocity, 0));
        bullet.GetComponent<Rigidbody>().velocity = (PlayerTarget.transform.position - bullet.transform.position).normalized * Velocity;
        //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * Velocity);
        timer = 0;
        Destroy(bullet, 3f);

    }

    private void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            nextDestination();
    }

}
