using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    public float Velocity = 1000f;
    GameObject Enemy;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 target = ray.GetPoint(distance);
                Vector3 direction = target - transform.position;
                float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, rotation, 0);
            }

            GameObject bullet = Instantiate(projectile, player.transform.position, player.transform.rotation);
            Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
            //bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, Velocity, 0));
            bullet.GetComponent<Rigidbody>().AddForce(player.transform.forward * Velocity);
            Destroy(bullet, 3f);


        }
    }

  
}
