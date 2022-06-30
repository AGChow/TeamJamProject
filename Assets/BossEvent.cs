using System.Collections;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    public GameObject player;
    public GameObject BossHitBox;
    public GameObject BossWeakSpot;
    public GameObject BossSlamAttackBox;
    public GameObject torchesObjects;
    public Transform projectileSpawnPoint;

    //Particles
    public GameObject _hitParticles;
    //prefab for projectile
    public GameObject ProjectilePrefab;

    public float Health = 15;

    public Vector3 _lookDirection;
    public float currentRotSpeed = 0f;
    public float topRotSpeed = 2f;

    public bool canFollow;
    public bool frozen;
    public bool knockedOut;


    public float rateOfShooting = .15f;
    public bool canShoot;
    public bool shooting;
    public bool canSlam;
    public bool slamming;
    public bool stunned;

    private BossPhaseManager bossPhaseManager;
    public BossWeakSpot bossWeakSpot;
    public Animator anim;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<Player>().gameObject;
        bossPhaseManager = GetComponent<BossPhaseManager>();
        StartCoroutine(Init());
        bossWeakSpot = GetComponentInChildren<BossWeakSpot>();
    }

    void Update()
    {
        if (canFollow == true && frozen == false)
        {
            TurnToTarget(player);
        }


        if(stunned == false) {
            if (canShoot == true && !shooting)
            {
                StartCoroutine(AttackLongRange());
            }
            
            if(canSlam == true && !slamming)
            {
                StartCoroutine(AttackShortRange());
            }

            if(shooting == true)
            {
                StopCoroutine(ShootProjectile());
                StartCoroutine(ShootProjectile());
            }
        }
        

    }

    IEnumerator Init()
    {
        //walk into room timing
        yield return new WaitForSeconds(5);
        //roar animation and camera sweep
        GetComponentInChildren<Animator>().SetTrigger("StartIntro");
        yield return new WaitForSeconds(8);
        canFollow = true;
        player.GetComponent<PlayerMovement>()._canMove = true;
        bossPhaseManager.SetBossPhase(1);
    }

    public void TakeDamage()
    {
        Instantiate(_hitParticles, BossHitBox.transform.position, BossHitBox.transform.rotation);
        StartCoroutine(GetComponentInChildren<MaterialChange>().FlashWhite());
        StartCoroutine(HitTimePauseBossHitBox());
        FindObjectOfType<AudioManager>().Play("BossGrunt");

        //animation
        anim.SetTrigger("StunnedHurt");

        Health = Health - 1;

        switch(Health) {
            case 10:
                BossHitBox.SetActive(false);
                //Getback up animation
                canFollow = true;
                //reset stun hitbox
                BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
                anim.SetBool("Stunned", false);
                anim.SetTrigger("StunnedRecover");
                bossPhaseManager.SetBossPhase(2);
                break;
            case 5:
                BossHitBox.SetActive(false);
                //Getback up animation
                canFollow = true;
                //reset stun hitbox
                BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
                anim.SetBool("Stunned", false);
                anim.SetTrigger("StunnedRecover");
                bossPhaseManager.SetBossPhase(3);
                break;
            case 0:
                BossHitBox.SetActive(false);
                StartCoroutine(BossDeath());
                break;
            default:
                break;
        }
    }
    public IEnumerator BossDeath()
    {
        Timer.instance.StopAndRecordTime();
        //TODO: move object down so player can't run into it

        StopAllCoroutines();
        shooting = false;
        canShoot = false;
        canFollow = false;
        
        Debug.Log("KilledBoss");
        FindObjectOfType<AudioManager>().Play("BossScream");
        //destroy and make a big deal about it
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().Play("BossScream");
        yield return new WaitForSeconds(2f);

        FindObjectOfType<BossRoomManager>().GetComponent<BossRoomManager>().CheckCompleteConditions();
        //open door now

    }

    public void TurnToTarget(GameObject Target)
    {
        // distance between target and the actual rotating object
        Vector3 D = Target.transform.position - transform.position;


        // calculate the Quaternion for the rotation
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), currentRotSpeed * Time.deltaTime);

        //Apply the rotation 
        transform.rotation = rot;

        // put 0 on the axys you do not want for the rotation object to rotate
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    public IEnumerator Freeze()
    {
        int currentBossPhase = bossPhaseManager.GetBossPhase();
        ExposeBack();

        frozen = true;
        //change animation to frozen
        anim.SetBool("Frozen", true);

        BossWeakSpot.SetActive(true);
        GetComponentInChildren<MaterialChange>().ChangeToAltMaterial();

        yield return new WaitForSeconds(4);
        //break out animation
        anim.SetBool("Frozen", false);

        GetComponentInChildren<MaterialChange>().ChangeBackToOrigingalMaterial();

            yield return new WaitForSeconds(1);
            //turn back to player
            frozen = false;

        // Player didn't get to the next phase yet after frozen period
        if (bossPhaseManager.GetBossPhase() == currentBossPhase)
        {
            bossPhaseManager.ResumePhase();
        }
    }

    public IEnumerator Stun()
    {
        stunned = true;
        int currentBossPhase = bossPhaseManager.GetBossPhase();
        StopAllCoroutines();
        shooting = false;
        canSlam = false;
        slamming = false;

        //make sure material is correct
        GetComponentInChildren<MaterialChange>().ChangeBackToOrigingalMaterial();

        //stun animation
        anim.SetBool("Stunned", true);
        anim.SetTrigger("StartStun");


        FindObjectOfType<AudioManager>().Play("BossScream");

        //turn off stun hitbox
        BossWeakSpot.GetComponent<BossWeakSpot>().TurnOffHitBox();
        frozen = false;
        canFollow = false;

        
        yield return new WaitForSeconds(3f);
        //activate hitbox
        BossHitBox.SetActive(true);
        yield return new WaitForSeconds(4f);
        BossHitBox.SetActive(false);
        anim.SetBool("Stunned", false);
        anim.SetTrigger("StunnedRecover");


        //Getback up animation
        yield return new WaitForSeconds(4f);

        yield return new WaitForSeconds(2f);
        //reset stun hitbox
        BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
        canFollow = true;
        yield return new WaitForSeconds(2f);

        // Player didn't get to the next phase yet after stun period
        if(bossPhaseManager.GetBossPhase() == currentBossPhase) {
            bossPhaseManager.ResumePhase();
        }
        stunned = false;
    }

    public IEnumerator HitTimePauseWeakSpot()
    {
        // disable the collider during hit so that it doesn't keep getting triggered as it passes through
        BossWeakSpot.GetComponent<BossWeakSpot>().TurnOffHitBox();
        Time.timeScale = .1f;
        yield return new WaitForSeconds(.04f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(.3f);
        BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
    }
    public IEnumerator HitTimePauseBossHitBox()
    {
        // disable the collider during hit so that it doesn't keep getting triggered as it passes through
        BossHitBox.GetComponent<Collider>().enabled = false;

        Time.timeScale = .1f;
        yield return new WaitForSeconds(.04f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(.3f);
        BossHitBox.GetComponent<Collider>().enabled = true;
    }

    public IEnumerator AttackLongRange()
    {
        canShoot = false;
        //start animation charging up shooting attack
        anim.SetTrigger("StartProjectile");
        yield return new WaitForSeconds(4);
        //start shooting
        shooting = true;

        yield return new WaitForSeconds(8);
        anim.SetTrigger("EndProjectile");
        yield return new WaitForSeconds(1);
        //stop shooting
        shooting = false;
        StopCoroutine(ShootProjectile());
        shooting = false;

        //StopAllCoroutines();

        yield return new WaitForSeconds(5);
    }
    public IEnumerator ShootProjectile()
    {

        shooting = false;

        Instantiate(ProjectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);

        yield return new WaitForSeconds(rateOfShooting);
        shooting = true;
    }
    public IEnumerator InitPhase1()
    {
        yield return new WaitForSeconds(1f);
        canSlam = true;
        StartCoroutine(AttackShortRange());
    }
    public IEnumerator InitPhase2()
    {
        yield return new WaitForSeconds(2f);
        canSlam = false;
        canShoot = true;
    }
    public IEnumerator InitPhase3()
    {
        yield return new WaitForSeconds(2f);
        BringDownTorches();
        CoverBack();
        rateOfShooting = .2f;
        canShoot = true;
    }
    public IEnumerator ResumePhase1() {
        yield return new WaitForSeconds(2f);
        canSlam = true;
        slamming = false;
    }
    public IEnumerator ResumePhase2() {
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    public IEnumerator ResumePhase3() {
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
    public IEnumerator AttackShortRange()
    {
        canSlam = true;
        slamming = true;

        //start animation wind up
        anim.SetTrigger("SlamAttack");
        yield return new WaitForSeconds(3f);
        //slam
        currentRotSpeed = 0;
        yield return new WaitForSeconds(.3f);
        BossSlamAttackBox.SetActive(true);
        yield return new WaitForSeconds(1f);
        BossSlamAttackBox.SetActive(false);
        //return to idle anim
        currentRotSpeed = topRotSpeed;

        yield return new WaitForSeconds(3);
        slamming = false;
    }


    public void BringDownTorches()
    {
        torchesObjects.GetComponent<Animator>().SetTrigger("Drop");
    }

    public void CoverBack()
    {
        anim.SetBool("CoveringWeakSpot", true);
        bossWeakSpot.TurnOffHitBox();
    }
    public void ExposeBack()
    {
        anim.SetBool("CoveringWeakSpot", false);
        bossWeakSpot.TurnOnHitBox();
    }

}
