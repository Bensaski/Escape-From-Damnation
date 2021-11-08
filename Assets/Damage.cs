using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
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

        if (other.CompareTag("Enemy"))
        {
            Enemy Enemy = other.gameObject.GetComponent<Enemy>();
            GameObject firework = Instantiate(hit, other.transform.position, Quaternion.identity);
            firework.GetComponent<ParticleSystem>().Play();

            Enemy.gameObject.SendMessage("recieveDamage", 1);
            Destroy(gameObject);
        }
    }
}
