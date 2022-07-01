using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject _debrisParticles;


    public void ObjectDestruction()
    {
        FindObjectOfType<CameraShake>().ScreenShake(.3f, .1f, 1);

        Instantiate(_debrisParticles, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("placeholder");
        Destroy(this.gameObject);
    }
}
