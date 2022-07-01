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
    public GameObject _damageAreaParticles;
    public GameObject _slamHitParticles;
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
    public BossEyeball bossEyeBallAnim;
    public Animator anim;
    public AudioClip bossMusic;
    public AudioClip finishBossMusic;

    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
        player.GetComponent<Player>().timeToMove = 12;
        
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
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
                StopCoroutine(AttackLongRange());
                StartCoroutine(AttackLongRange());
            }
            
            if(canSlam == true && !slamming)
            {
                StopCoroutine(AttackShortRange());
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
        yield return new WaitForSeconds(4);
        AudioManager.instance.musicAudioClip = bossMusic;
        yield return new WaitForSeconds(4);
        canFollow = true;
        //player.GetComponent<PlayerMovement>()._canMove = true;
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
                if (Health > 10)
                {
                    return;
                }
                else
                {

                BossHitBox.SetActive(false);
                //reset stun hitbox
                BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
                //Getback up animation
                anim.SetBool("Stunned", false);
                anim.SetTrigger("StunnedRecover");
                stunned = false;
                bossPhaseManager.SetBossPhase(2);
                break;
                }
            case 5:
                if (Health > 5)
                {
                    return;
                }
                else
                {
                    BossHitBox.SetActive(false);
                    //reset stun hitbox
                    BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
                    //Getback up animation
                    anim.SetBool("Stunned", false);
                    anim.SetTrigger("StunnedRecover");
                    stunned = false;
                    bossPhaseManager.SetBossPhase(3);
                    break;
                }
            case 0:
                if (Health > 0)
                {
                    return;
                }
                else
                {
                    BossHitBox.SetActive(false);
                    StartCoroutine(BossDeath());
                    bossPhaseManager.SetBossPhase(4);
                    break;
                }
            default:
                break;
        }
    }
    public IEnumerator BossDeath()
    {
        if(Timer.instance != null)
            Timer.instance.StopAndRecordTime();

        StopSelectedCoroutines();
        StopCoroutine(Freeze());
        StopCoroutine(Stun());
        stunned = false;
        shooting = false;
        canShoot = false;
        canFollow = false;
        
        FindObjectOfType<AudioManager>().Play("BossScream");
        //destroy and make a big deal about it
        anim.SetBool("Dead", true);
        yield return new WaitForSeconds(2f);
        FindObjectOfType<AudioManager>().Play("BossScream");
        yield return new WaitForSeconds(2f);
        //moves it down so player can't run into anything
        transform.position = new Vector3(transform.position.x, -20, transform.position.z);

        FindObjectOfType<BossRoomManager>().GetComponent<BossRoomManager>().RoomCleared();
        AudioManager.instance.musicAudioClip = finishBossMusic;

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
        StopSelectedCoroutines();
        stunned = true;
        shooting = false;
        canShoot = false;
        canSlam = false;
        slamming = false;
        frozen = true;
        StopSelectedCoroutines();

        //change animation to frozen
        anim.SetBool("Frozen", true);
        ExposeBack();

        _damageAreaParticles.SetActive(false);
        GetComponentInChildren<MaterialChange>().ChangeToAltMaterial();

        yield return new WaitForSeconds(8);
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
        int currentBossPhase = bossPhaseManager.GetBossPhase();
        canFollow = false;

        StopSelectedCoroutines();
        stunned = true;
        shooting = false;
        canShoot = false;
        canSlam = false;
        slamming = false;
        frozen = false;
        canFollow = false;
        StopSelectedCoroutines();

        TurnOffSlamHitBox();
        _damageAreaParticles.SetActive(false);
        //turn off stun hitbox
        BossWeakSpot.GetComponent<BossWeakSpot>().TurnOffHitBox();
        ExposeBack();
        yield return new WaitForSeconds(.5f);
        bossEyeBallAnim.CloseEye();




        //make sure material is correct
        GetComponentInChildren<MaterialChange>().ChangeBackToOrigingalMaterial();

        //stun animation
        anim.SetBool("Frozen", false);
        anim.SetTrigger("StartStun");
        anim.SetBool("Stunned", true);


        FindObjectOfType<AudioManager>().Play("BossScream");

        

        
        yield return new WaitForSeconds(3f);

        //activate hitbox
        BossHitBox.SetActive(true);
        yield return new WaitForSeconds(4f);
        BossHitBox.SetActive(false);
        anim.SetBool("Stunned", false);
        anim.SetTrigger("StunnedRecover");


        //Getback up animation
        yield return new WaitForSeconds(6f);

        //reset stun hitbox
        if(bossPhaseManager.phase != 3)
        {
            BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();

        }
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
        canFollow = true;
        canSlam = true;
        slamming = false;
        bossEyeBallAnim.OpenEye();

        StartCoroutine(AttackShortRange());
    }
    public IEnumerator InitPhase2()
    {
        yield return new WaitForSeconds(2f);
        canFollow = true;
        canSlam = false;
        canShoot = true;
        bossEyeBallAnim.OpenEye();

    }
    public IEnumerator InitPhase3()
    {
        shooting = false;
        slamming = false;
        yield return new WaitForSeconds(2f);
        BringDownTorches();
        CoverBack();
        canFollow = true;
        rateOfShooting = .2f;
        canShoot = true;
        bossEyeBallAnim.CloseEye();

    }
    public IEnumerator ResumePhase1() {
        yield return new WaitForSeconds(2f);
        canFollow = true;
        canSlam = true;
        slamming = false;
        bossEyeBallAnim.OpenEye();

        StartCoroutine(AttackShortRange());

    }
    public IEnumerator ResumePhase2() {
        yield return new WaitForSeconds(2f);
        canFollow = true;
        canSlam = false;
        canShoot = true;

        bossEyeBallAnim.OpenEye();

    }
    public IEnumerator ResumePhase3() {
        yield return new WaitForSeconds(3f);
        CoverBack();
        canFollow = true;
        bossEyeBallAnim.CloseEye();

        //rateOfShooting = .2f;
        canShoot = true;
    }
    public IEnumerator AttackShortRange()
    {
        canSlam = true;
        slamming = true;
        _damageAreaParticles.SetActive(true);
        _damageAreaParticles.GetComponent<ParticleSystem>().Play();

        //start animation wind up
        anim.SetTrigger("SlamAttack");
        yield return new WaitForSeconds(2.5f);
        //slam
        currentRotSpeed = 0;
        _damageAreaParticles.SetActive(false);


        yield return new WaitForSeconds(1.8f);
        //return to idle anim automatic
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
        bossEyeBallAnim.CloseEye();
    }
    public void ExposeBack()
    {
        anim.SetBool("CoveringWeakSpot", false);
        bossWeakSpot.TurnOnHitBox();
        bossEyeBallAnim.OpenEye();
    }

    public void TurnOnSlamHitBox()
    {
        BossSlamAttackBox.SetActive(true);
        _slamHitParticles.GetComponent<ParticleSystem>().Play();
        FindObjectOfType<AudioManager>().Play("placeholder");

    }
    public void TurnOffSlamHitBox()
    {
        BossSlamAttackBox.SetActive(false);
        _damageAreaParticles.SetActive(false);

    }
    public void StopSelectedCoroutines()
    {
        StopCoroutine(AttackLongRange());
        StopCoroutine(ShootProjectile());
        StopCoroutine(InitPhase1());
        StopCoroutine(InitPhase2());
        StopCoroutine(InitPhase3());
        StopCoroutine(ResumePhase1());
        StopCoroutine(ResumePhase2());
        StopCoroutine(ResumePhase3());
        StopCoroutine(AttackShortRange());
    }

}
