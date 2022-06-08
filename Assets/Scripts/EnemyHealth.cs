using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float Health = 1f;

    public void takeDamage()
    {
        Health = Health - 1;
        CheckHealth();
    }
    public void CheckHealth()
    {
        if(Health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
