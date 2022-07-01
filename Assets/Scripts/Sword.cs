using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack _playerAttack;
    [SerializeField]
    private Animator _playerAnimator;
    //private bool _swungAtShield = false;
    public bool canSwing;
    private bool preventExtraTorchToggles;

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
        /* if (other.CompareTag("Shield"))
            _swungAtShield = true;
        else */ if (other.CompareTag("Torch")) {
            if(!preventExtraTorchToggles) {
                other.GetComponent<Torch>().ToggleTorch();
                preventExtraTorchToggles = true;
                StartCoroutine(PreventExtraTorchToggles());
            }
        }
        else if (other.CompareTag("BossWeakSpot"))
            HandleBossWeakSpotCollision(other.gameObject);
        else if (other.CompareTag("BossHitBox"))
            HandleBossHitBoxCollision(other.gameObject);
        else if (other.CompareTag("Enemy"))
        {
            HandleEnemyCollision(other.gameObject);
            /* if (other.GetComponent<ShieldEnemy>()) {
                ShieldEnemy _shieldEnemy = other.GetComponent<ShieldEnemy>();
                if (_shieldEnemy.IsShielded() && _swungAtShield)
                else

                _swungAtShield = false;
            }
            else
            {
                HandleEnemyCollision(other.gameObject);
            } */
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

    IEnumerator PreventExtraTorchToggles() {
        yield return new WaitForSeconds(.2f);
        preventExtraTorchToggles = false;
    }

    private void HandleEnemyCollision(GameObject enemyHit)
    {
        FindObjectOfType<AudioManager>().Play("placeholder");
        FindObjectOfType<CameraShake>().ScreenShake(.2f, .25f, 1);

        enemyHit.GetComponent<EnemyHealth>().takeDamage();
    }


    private void HandleBreakableCollision(GameObject breakableObj)
    {
        FindObjectOfType<AudioManager>().Play("placeholder");

        breakableObj.GetComponent<BreakableObject>().ObjectDestruction();
    }

    private void HandleBossWeakSpotCollision(GameObject weakSpot)
    {
        FindObjectOfType<CameraShake>().ScreenShake(.2f, .25f, 1);

        weakSpot.GetComponent<BossWeakSpot>().StunHit();
    }
    private void HandleBossHitBoxCollision(GameObject hitbox)
    {
        hitbox.GetComponentInParent<BossEvent>().TakeDamage();
        FindObjectOfType<CameraShake>().ScreenShake(.2f, .25f, 1);

        FindObjectOfType<Player>().GetComponentInChildren<PlayerAttack>().RecallWeapon();

    }
}
