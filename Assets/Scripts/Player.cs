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

    private Transform hearts;
    [SerializeField]
    private Sprite fullHeart;
    [SerializeField]
    private Sprite emptyHeart;

    void Awake()
    {
        Heal(_playerMaxHealth);
        hearts = GameObject.Find("HealthUI").transform;
    }

    public void Damage(int dmg)
    {
        if(_isInvincible) return;

        _playerCurrentHealth -= dmg;

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
        GetComponent<CharacterController>().enabled = false;
        gameOver.SetActive(true);
    }

    IEnumerator Invincible()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(3f);
        _isInvincible = false;
    }
}
