using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakSpot : MonoBehaviour
{
    public GameObject hitparticles;
    public float maxArmor = 3;
    public float currentArmor = 3;
    private BossEvent _bossEvent;

    void Awake() {
        _bossEvent = GetComponentInParent<BossEvent>();
    }
    
    
    //this is for the bosses weakspot. He doesn't take damage, just goes into a stunned 
    //state for the player to wail on him
    public void StunHit()
    {

        // Changed to <=1 because if we check 0, we haven't subtracted the current armor yet, so they end up getting one extra armor
        if(currentArmor <= 1)
        {
            FindObjectOfType<AudioManager>().Play("BossScream");
            Instantiate(hitparticles, transform.position, transform.rotation);

            //reset armor for next round
            resetArmor();
            //breaks armor. Start stun event
            StartCoroutine(_bossEvent.Stun());
        }
        else
        {
            _bossEvent.anim.SetTrigger("WeakSpotHit");

            Instantiate(hitparticles, transform.position, transform.rotation);
            currentArmor -= 1;
            //replace with boss grunt
            FindObjectOfType<AudioManager>().Play("BossGrunt");

            //StartCoroutine(GetComponentInParent<BossEvent>().Flash());
            StartCoroutine(_bossEvent.HitTimePauseWeakSpot());
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
