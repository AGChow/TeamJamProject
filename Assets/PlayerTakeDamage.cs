using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage: MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<Player>().Damage(1);
        else if (other.gameObject.CompareTag("Sword"))
            FindObjectOfType<Player>().GetComponent<PlayerAttack>().RecallWeapon();
        else if (other.gameObject.CompareTag("Torch"))
            other.gameObject.GetComponent<Torch>().ExtinguishTorch();

    }
}
