using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakSpot : MonoBehaviour
{
    public GameObject hitparticles;
    private float maxArmor = 3;
    private float currentArmor = 3;
    
    
    //this is for the bosses weakspot. He doesn't take damage, just goes into a stunned 
    //state for the player to wail on him
    public void StunHit()
    {

        if(currentArmor <= 0)
        {
            //breaks armor. Start stun event
            StartCoroutine(GetComponentInParent<BossEvent>().Stun());
            //reset armor for next round
            resetArmor();
        }
        else
        {
            Instantiate(hitparticles, transform.position, transform.rotation);
            currentArmor -= 1;
            //replace with boss grunt
            FindObjectOfType<AudioManager>().Play("placeholder");

            //StartCoroutine(GetComponentInParent<BossEvent>().Flash());
            StartCoroutine(GetComponentInParent<BossEvent>().HitTimePauseWeakSpot());
        }
    }
    public void resetArmor()
    {
        currentArmor = maxArmor;
    }

    public void TurnOffHitBox()
    {
        GetComponent<Collider>().enabled = false;
    }
    public void TurnOnHitBox()
    {
        GetComponent<Collider>().enabled = true;
    }
}
