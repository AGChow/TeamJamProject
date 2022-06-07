using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject _debrisParticles;


    public void ObjectDestruction()
    {
        Instantiate(_debrisParticles, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("placeholder");
        Destroy(this.gameObject);
    }
}
