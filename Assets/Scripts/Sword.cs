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

    public void SwordSwing()
    {
        _collider.enabled = true;
        StartCoroutine(Swing());
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Shield"))
        {
            print("Swung at shield. Nothing happens.");
            //TODO: Check if the sword hit an enemy before the shield, which would indicate being hit from behind
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

        // Wait until end of sword-swinging animation - can be uncommented and replace the hard-coded value below when animation is implemented
        //yield return new WaitForSeconds(_playerAnimator.GetCurrentAnimatorStateInfo(0).length
        //    + _playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime
        //);
        yield return new WaitForSeconds(0.25f);

        _collider.enabled = false;
    }
}
