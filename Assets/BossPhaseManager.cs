using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseManager : MonoBehaviour
{
    public bool phase1;
    public bool phase2;
    public bool phase3;
    public bool looping;

    public BossEvent BE;
    public BossRoomManager BRM;

    // Start is called before the first frame update
    void Start()
    {
        BRM = FindObjectOfType<BossRoomManager>().gameObject.GetComponent<BossRoomManager>();
        BE = GetComponent<BossEvent>();

        phase1 = true;
        looping = true;
        phase2 = false;
        phase3 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(phase1 == true && looping == true)
        {

        }
    }
    //phase 1, no torches, back exposed, slams hand to attack player
    //phase 2, back exposed, shoots projectiles and slams hand
    //phase 3, covers back, torches come down, shoots projectiles, slams hands

    public IEnumerator Phase1Loop()
    {
        looping = false;
        //loop through this. Idle. AttackLongRange. Open weakspot
        yield return new WaitForSeconds(5);
        BE.canShoot = true;
        BE.shooting = true;



        looping = true;
    }
    void Phase2Init()
    {
        BE.BringDownTorches();

    }

    public IEnumerator Phase2Loop()
    {
        yield return new WaitForSeconds(3);

    }
    void Phase3Logic()
    {

    }
}
