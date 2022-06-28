using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8;
    public float timer;
    public GameObject explosionParticles;


    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        timer += Time.deltaTime;

        if(timer > 5)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().Damage(1);
        }
        Instantiate(explosionParticles, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
