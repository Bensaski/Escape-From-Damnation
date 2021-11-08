using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDamage : MonoBehaviour
{

    public GameObject hit;
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

        if (other.CompareTag("Player"))
        {
            GameObject firework = Instantiate(hit, other.transform.position, other.transform.rotation);
            firework.GetComponent<ParticleSystem>().Play();
            Enemy Enemy = other.gameObject.GetComponent<Enemy>();

            Enemy.gameObject.SendMessage("recieveDamage", 1);


            Destroy(gameObject);
        }
    }
}
