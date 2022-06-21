using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    public GameObject player;
    public float Health = 10;
    public float armor = 3;

    public Vector3 _lookDirection;
    public float rotSpeed = 0f;

    public bool canFollow;
    public bool frozen;
    public bool knockedOut;


    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        StartCoroutine(Init());
    }

    // Update is called once per frame
    void Update()
    {
        if (canFollow == true && frozen == false)
        {
            TurnToTarget(player);
        }

    }

    IEnumerator Init()
    {
        //walk into room timing
        yield return new WaitForSeconds(5);
        //roar animation and camera sweep
        yield return new WaitForSeconds(5);
        canFollow = true;
    }

    public void TakeDamage()
    {
        
        Health = Health - 1;
        if (Health <= 0)
        {
            //death animation
            BossDeath();
        }
        else
        {
            if (frozen)
            {
                //Flash
            }
            else if(knockedOut)
            {

                //damage animation
            }
        }
    }
    public void BossDeath()
    {
        //destroy and make a big deal about it
    }

    public void TurnToTarget(GameObject Target)
    {
        // distance between target and the actual rotating object
        Vector3 D = Target.transform.position - transform.position;


        // calculate the Quaternion for the rotation
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), rotSpeed * Time.deltaTime);

        //Apply the rotation 
        transform.rotation = rot;

        // put 0 on the axys you do not want for the rotation object to rotate
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    public IEnumerator Freeze()
    {
            frozen = true;
            //change animation to frozen
            yield return new WaitForSeconds(4);
            //break out animation
            yield return new WaitForSeconds(1);
            //turn back to player
            frozen = false;
    }
}
