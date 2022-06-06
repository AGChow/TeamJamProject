using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack _playerAttack;
    [SerializeField]
    private Animator _playerAnimator;

    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    void OnEnable()
    {
        StartCoroutine(Swing());
    }

    void OnTriggerEnter(Collider other)
    {
        print("Swinging?");
        if(other.CompareTag("Shield"))
        {
            return;
        }
        if(other.CompareTag("Torch"))
        {
            other.GetComponent<Torch>().ToggleTorch();
        }
        if(other.CompareTag("Enemy"))
        {
            print("Damaged enemy: " + other.name);
        }

    }

    IEnumerator Swing()
    {
        _playerAnimator.SetTrigger("SwingSword");

        // Wait until end of sword-swinging animation
        yield return new WaitForSeconds(_playerAnimator.GetCurrentAnimatorStateInfo(0).length
            + _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime
        );

        this.enabled = false;
    }
}
