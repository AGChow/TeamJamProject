using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _playerMaxHealth = 3;
    private int _playerCurrentHealth = 3;
    private bool _isInvincible = false;

    [SerializeField]
    private GameObject gameOver;

    void Awake()
    {
        Heal(_playerMaxHealth);
    }

    public void Damage(int dmg)
    {
        if(_isInvincible) return;

        _playerCurrentHealth -= dmg;
        print("Player took damage! New health: " + _playerCurrentHealth);
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
        gameOver.SetActive(true);
    }

    IEnumerator Invincible()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(3f);
        _isInvincible = false;
    }
}
