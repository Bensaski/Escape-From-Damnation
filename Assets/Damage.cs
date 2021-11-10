using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject hit;
    public AudioClip clip;
    public float volume = 0.5f;
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
            GameObject explosion = Instantiate(hit, other.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(clip, other.transform.position, volume);
            explosion.GetComponent<ParticleSystem>().Play();


            Enemy.gameObject.SendMessage("recieveDamage", 1);
            Destroy(gameObject);
        }
    }
}
