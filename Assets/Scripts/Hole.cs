using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(other.GetComponent<Player>().Fall());
            //other.GetComponent<Player>().Fall();
            //other.GetComponent<Player>().GameOver();
            //Destroy(other);
        }
    }
}
