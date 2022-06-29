using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _playerMaxHealth = 3;
    private int _playerCurrentHealth = 3;
    private bool _isInvincible = false;

    [SerializeField]
    private GameObject gameOver;

    private Animator _playerAnimator;

    private Transform hearts;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite emptyHeart;

    void Awake()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        Heal(_playerMaxHealth);
        hearts = GameObject.Find("HealthUI").transform;
        StartCoroutine(Intro());
    }

    public void Damage(int dmg)
    {
        if(_isInvincible) return;

        _playerCurrentHealth -= dmg;

        //switch placeholder audio to player grunt
        FindObjectOfType<AudioManager>().Play("placeholder");
        FindObjectOfType<AudioManager>().Play("PlayerGetsHit");

        StartCoroutine(HitTimePause());
        StartCoroutine(GetComponentInChildren<MaterialChange>().FlashWhite());

        // Update UI hearts
        bool shakenHeart = false;
        for(int i = 0; i < _playerMaxHealth; i++)
        {
            if(i < _playerCurrentHealth)
                hearts.GetChild(i).GetComponent<Image>().sprite = fullHeart;
            else
            {
                if(!shakenHeart)
                {
                    hearts.GetChild(i).GetComponent<Animator>().SetTrigger("HeartShake");
                    shakenHeart = true;
                }
                hearts.GetChild(i).GetComponent<Image>().sprite = emptyHeart;
            }
        }

        if(_playerCurrentHealth <= 0)
        {
            GetComponent<Collider>().enabled = false;
            GameOver();
        }
        else
        {
            StartCoroutine(Invincible());
        }
    }

    public void Heal(int heal)
    {
        _playerCurrentHealth += heal;
        if(_playerCurrentHealth > _playerMaxHealth)
        {
            _playerCurrentHealth = _playerMaxHealth;
        }
    }

    public void GameOver()
    {
        _playerAnimator.SetTrigger("Death");
        GetComponent<PlayerMovement>().Death();
        GetComponent<PlayerMovement>()._canMove = false;

        GetComponent<CharacterController>().enabled = false;
        gameOver.SetActive(true);
    }

    IEnumerator Invincible()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(3f);
        _isInvincible = false;
    }

    public IEnumerator Fall()
    {
        _playerAnimator.SetTrigger("Fall");
        FindObjectOfType<AudioManager>().Play("PlayerFall");
        yield return new WaitForSeconds(1f);

        gameOver.SetActive(true);
        yield return new WaitForSeconds(1f);
        GetComponent<PlayerMovement>()._canMove = false;
        GetComponent<CharacterController>().enabled = false;

    }

    IEnumerator Intro()
    {
        _playerAnimator.SetTrigger("Intro");

        GetComponent<PlayerMovement>().StopMovement();

        yield return new WaitForSeconds(3.5f);

        Timer.instance.StartTimer();
        GetComponent<PlayerMovement>().ResetMovement();
        GetComponent<PlayerAttack>().canAttack = true;
    }

    public IEnumerator HitTimePause()
    {

        Time.timeScale = .1f;
        yield return new WaitForSeconds(.04f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(.3f);
    }


}
