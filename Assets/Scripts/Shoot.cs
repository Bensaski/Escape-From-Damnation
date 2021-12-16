using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public AudioClip shoot;
    public GameObject projectile;
    public GameObject player;
    GameObject bullet;
    public GameObject BulletSpawner;
    public float Velocity = 1000f;
    GameObject Enemy;
    Animator animator;
    public bool shooting;

    private Collider[] childrenColliders;
    void Start()
    {
        childrenColliders = GetComponentsInChildren<Collider>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioSource.PlayClipAtPoint(shoot, player.transform.position, 0.5f);
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
            animator.SetBool("Shooting", true);

            if (PlayerPrefs.GetInt("Shots") == 2) //Decides how many bullets to shoot based on the PlayerPrefs 
            {
                
                GameObject bullet = Instantiate(projectile, new Vector3(BulletSpawner.transform.position.x - 0.5f, BulletSpawner.transform.position.y, BulletSpawner.transform.position.z), BulletSpawner.transform.rotation);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), player.GetComponent<Collider>(), true);

                GameObject bullet2 = Instantiate(projectile, new Vector3(BulletSpawner.transform.position.x + 0.5f, BulletSpawner.transform.position.y, BulletSpawner.transform.position.z), BulletSpawner.transform.rotation);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
                bullet.GetComponent<Rigidbody>().AddForce(player.transform.forward * Velocity);
                bullet2.GetComponent<Rigidbody>().AddForce(player.transform.forward * Velocity);
            }
            else
            {

                GameObject bullet = Instantiate(projectile, BulletSpawner.transform.position, BulletSpawner.transform.rotation);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
                bullet.GetComponent<Rigidbody>().AddForce(player.transform.forward * Velocity);
            }
 
            foreach (Collider col in childrenColliders)
            //bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, Velocity, 0));

            Invoke("ShootingFalse",0.1f);
            Destroy(bullet, 3f);


        }
    }


    void ShootingFalse()
    {
        animator.SetBool("Shooting", false);
    }
  
}
