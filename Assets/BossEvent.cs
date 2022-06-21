using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    public GameObject player;
    public float Health = 10;

    public Vector3 _lookDirection;

    public bool canFollow;
    public bool frozen;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFollow == true && frozen == false)
        {
            _lookDirection = player.transform.position;
            transform.LookAt(new Vector3(_lookDirection.x, transform.position.y, _lookDirection.z));
        }
    }

    IEnumerator init()
    {
        yield return new WaitForSeconds(10);
        canFollow = true;
    }

    public void TakeDamage()
    {
        Health = Health - 1;
        if (Health <= 0)
        {
            BossDeath();
        }
    }
    public void BossDeath()
    {
        //destroy and make a big deal about it
    }
}
