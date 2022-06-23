using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseManager : MonoBehaviour
{
    public bool phase1;
    public bool phase2;
    public bool phase3;
    // Start is called before the first frame update
    void Start()
    {
        phase1 = true;
        phase2 = false;
        phase3 = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //phase 1, no torches, back exposed, slams hand to attack player
    //phase 2, back exposed, shoots projectiles and slams hand
    //phase 3, covers back, torches come down, shoots projectiles, slams hands
}
