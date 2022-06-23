using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    
    //this is for the bosses weakspot. He doesn't take damage, just goes into a stunned 
    //state for the player to wail on him
    public void StunHit()
    {
        StartCoroutine(GetComponentInParent<BossEvent>().Stun());
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
