using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneEnemy : MonoBehaviour
{
    private GameObject player;

    [SerializeField]
    private float enemySpeed;
    public bool frozen;
    private float agroTimer;
    public float attack1Range = 2;
    private Vector3 startPos;


    private Detection detectBools;

    // Start is called before the first frame update
    void Start()
    {
        detectBools = GetComponentInChildren<Detection>();
        player = GameObject.FindGameObjectWithTag("Player");
        startPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (detectBools.torchReaction == true)
        {
            freeze();
        }
        else
        {
            unfreeze();
        }
        if (detectBools.agro == true && frozen == false)
        {
            moveToPlayer();
        }

        else
        {
            rest(); 
        }
    }

    void moveToPlayer()
    {
        //rotate to look at player
        transform.LookAt(player.transform.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        //move towards player
        if (Vector3.Distance(transform.position, player.transform.position) > attack1Range)
        {
            transform.Translate(new Vector3(enemySpeed * Time.deltaTime, 0, 0));
        }

    }

    public void rest()
    {

        //move towards player
        if (Vector3.Distance(transform.position, startPos) > 1)
        {
            transform.Translate(new Vector3(enemySpeed * Time.deltaTime, 0, 0));

            //rotate towards destination
            transform.LookAt(startPos);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        }
    }

    private void freeze()
    {
        //can push
    }
    private void unfreeze()
    {
        //can't push
    }
}
