using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationEvents : MonoBehaviour
{
    public void BossGrowl()
    {
        FindObjectOfType<AudioManager>().Play("BossGrowl");

    }

    public void BossScream()
    {
        FindObjectOfType<AudioManager>().Play("BossScream");
    }
}
