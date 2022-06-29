using System.Collections;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    public GameObject player;
    public GameObject BossHitBox;
    public GameObject BossWeakSpot;
    public GameObject torchesObjects;
    public Transform projectileSpawnPoint;

    //Particles
    public GameObject _hitParticles;
    //prefab for projectile
    public GameObject ProjectilePrefab;

    public float Health = 15;
    public float armor = 3;

    public Vector3 _lookDirection;
    public float currentRotSpeed = 0f;
    public float topRotSpeed = 2f;

    public bool canFollow;
    public bool frozen;
    public bool knockedOut;


    public float rateOfShooting = .15f;
    public bool canShoot;
    public bool shooting;

    private BossPhaseManager bossPhaseManager;
    
    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        bossPhaseManager = GetComponent<BossPhaseManager>();
        StartCoroutine(Init());

    }

    void Update()
    {
        if (canFollow == true && frozen == false)
        {
            TurnToTarget(player);
        }


        if (shooting == true && canShoot == true)
        {
            StartCoroutine(ShootProjectile());
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
        bossPhaseManager.SetBossPhase(1);
    }

    public void TakeDamage()
    {
        Instantiate(_hitParticles, BossHitBox.transform.position, BossHitBox.transform.rotation);
        StartCoroutine(GetComponentInChildren<MaterialChange>().FlashWhite());
        StartCoroutine(HitTimePauseBossHitBox());

        Health = Health - 1;

        switch(Health) {
            case 10:
                BossHitBox.SetActive(false);
                //Getback up animation
                canFollow = true;
                //reset stun hitbox
                BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
                bossPhaseManager.SetBossPhase(2);
                break;
            case 5:
                BossHitBox.SetActive(false);
                //Getback up animation
                canFollow = true;
                //reset stun hitbox
                BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
                bossPhaseManager.SetBossPhase(3);
                break;
            case 0:
                BossHitBox.SetActive(false);
                BossDeath();
                break;
            default:
                bossPhaseManager.SetBossPhase(1);
                break;
        }
    }
    public void BossDeath()
    {
        Debug.Log("KilledBoss");
        //destroy and make a big deal about it
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
            frozen = true;
            //change animation to frozen
            BossWeakSpot.SetActive(true);
            GetComponentInChildren<MaterialChange>().ChangeToAltMaterial();

            yield return new WaitForSeconds(4);
            //break out animation
            GetComponentInChildren<MaterialChange>().ChangeBackToOrigingalMaterial();

            yield return new WaitForSeconds(1);
            //turn back to player
            frozen = false;
    }

    public IEnumerator Stun()
    {
        StopAllCoroutines();
        //make sure material is correct
        GetComponentInChildren<MaterialChange>().ChangeBackToOrigingalMaterial();

        //stun animation
        //turn on stun hitbox
        //replace with screech
        FindObjectOfType<AudioManager>().Play("placeholder");

        BossWeakSpot.GetComponent<BossWeakSpot>().TurnOffHitBox();
        frozen = false;
        canFollow = false;

        
        yield return new WaitForSeconds(2f);
        //activate hitbox
        BossHitBox.SetActive(true);
        yield return new WaitForSeconds(5f);
        BossHitBox.SetActive(false);
        yield return new WaitForSeconds(2f);
        //Getback up animation
        canFollow = true;
        //reset stun hitbox
        BossWeakSpot.GetComponent<BossWeakSpot>().TurnOnHitBox();
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
        canShoot = true;
        //start animation charging up shooting attack
        yield return new WaitForSeconds(1);
        //start shooting
        shooting = true;
        currentRotSpeed /= 1.5f;


        yield return new WaitForSeconds(8);
        //stop shooting
        canShoot = false;
        currentRotSpeed = topRotSpeed;
    }
    public IEnumerator AttackShortRange()
    {
        //start animation wind up
        yield return new WaitForSeconds(3);
        //slam
        currentRotSpeed = 0;
        yield return new WaitForSeconds(.5f);
        BossHitBox.SetActive(true);
        yield return new WaitForSeconds(1f);
        BossHitBox.SetActive(false);
        //return to idle anim
        currentRotSpeed = topRotSpeed;
    }
    public IEnumerator ShootProjectile()
    {
        shooting = false;

        Instantiate(ProjectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);

        yield return new WaitForSeconds(rateOfShooting);
        shooting = true;
    }

    public void BringDownTorches()
    {
        torchesObjects.GetComponent<Animator>().SetTrigger("Drop");
    }
   
}
