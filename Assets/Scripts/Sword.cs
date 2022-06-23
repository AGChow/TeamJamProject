using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack _playerAttack;
    [SerializeField]
    private Animator _playerAnimator;
    private bool _swungAtShield = false;
    public bool canSwing;

    [SerializeField]
    private TrailRenderer swingTrail;

    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
        canSwing = true;
    }

    public void SwordSwing()
    {
        if (canSwing == true) { 
        _collider.enabled = true;
        StartCoroutine(Swing());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield"))
            _swungAtShield = true;
        else if (other.CompareTag("Torch"))
            other.GetComponent<Torch>().ToggleTorch();
        else if (other.CompareTag("BossWeakSpot"))
            HandleBossWeakSpotCollision(other.gameObject);
        else if (other.CompareTag("BossHitBox"))
            HandleBossHitBoxCollision(other.gameObject);
        else if (other.CompareTag("Enemy"))
        {

            if (other.GetComponent<ShieldEnemy>()) {
                ShieldEnemy _shieldEnemy = other.GetComponent<ShieldEnemy>();
                if (_shieldEnemy.IsShielded() && _swungAtShield)
                    print("Hit shield");
                else
                    print("Damaged enemy: " + other.name);

                _swungAtShield = false;
            }
            else
            {
                HandleEnemyCollision(other.gameObject);
            }
        }

        else if (other.CompareTag("Breakable"))
            HandleBreakableCollision(other.gameObject);
    }

    IEnumerator Swing()
    {
        canSwing = false;
        FindObjectOfType<AudioManager>().Play("SwordSwing");

        _playerAnimator.SetTrigger("SwingSword");
        swingTrail.emitting = true;
        
        yield return new WaitForSeconds(0.2f);

        _collider.enabled = false;
        swingTrail.emitting = false;

        yield return new WaitForSeconds(.2f);
        canSwing = true;

    }


    private void HandleEnemyCollision(GameObject enemyHit)
    {
        print("hit enemy");
        FindObjectOfType<AudioManager>().Play("placeholder");
        FindObjectOfType<CameraShake>().ScreenShake(.2f, .5f, 1);

        enemyHit.GetComponent<EnemyHealth>().takeDamage();
    }


    private void HandleBreakableCollision(GameObject breakableObj)
    {
        Debug.Log("Break");
        FindObjectOfType<AudioManager>().Play("placeholder");

        breakableObj.GetComponent<BreakableObject>().ObjectDestruction();
    }

    private void HandleBossWeakSpotCollision(GameObject weakSpot)
    {
        weakSpot.GetComponent<BossWeakSpot>().StunHit();
    }
    private void HandleBossHitBoxCollision(GameObject hitbox)
    {
        hitbox.GetComponentInParent<BossEvent>().TakeDamage();
        FindObjectOfType<Player>().GetComponentInChildren<PlayerAttack>().RecallWeapon();

    }
}
