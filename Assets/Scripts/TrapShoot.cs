using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShoot : MonoBehaviour
{
    public GameObject projectile;
    float timer;
    int waitingTime = 2;
    public float Velocity = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>(), true);
        bullet.GetComponent<Rigidbody>().velocity = transform.up * Velocity;
        timer = 0;
        Destroy(bullet, 3f);
    }
}
