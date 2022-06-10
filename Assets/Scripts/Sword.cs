using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack _playerAttack;
    [SerializeField]
    private Animator _playerAnimator;
    private bool _swungAtShield = false;

    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    public void SwordSwing()
    {
        _collider.enabled = true;
        StartCoroutine(Swing());
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Shield"))
            _swungAtShield = true;
        if(other.CompareTag("Torch"))
            other.GetComponent<Torch>().ToggleTorch();
        if(other.CompareTag("Enemy"))
        {
            
            if(other.GetComponent<ShieldEnemy>()) {
                ShieldEnemy _shieldEnemy = other.GetComponent<ShieldEnemy>();
                if(_shieldEnemy.IsShielded() && _swungAtShield)
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
    }

    IEnumerator Swing()
    {
        _playerAnimator.SetTrigger("SwingSword");

        // Wait until end of sword-swinging animation - can be uncommented and replace the hard-coded value below when animation is implemented
        //yield return new WaitForSeconds(_playerAnimator.GetCurrentAnimatorStateInfo(0).length
        //    + _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime
        //);
        yield return new WaitForSeconds(0.3f);

        _collider.enabled = false;
    }


    private void HandleEnemyCollision(GameObject enemyHit)
    {
        print("hit enemy");
        FindObjectOfType<AudioManager>().Play("placeholder");
        FindObjectOfType<CameraShake>().ScreenShake(.3f, .8f, 1);

        enemyHit.GetComponent<EnemyHealth>().takeDamage();


    }
}
