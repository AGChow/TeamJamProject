using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float Health = 1f;
    public GameObject _poofParticles;
    public GameObject _hitParticles;

    public void takeDamage()
    {
        Health = Health - 1;
        Instantiate(_hitParticles, transform.position, transform.rotation);
        //below code means this script requires a material change componenet in the object with the renderer
        StartCoroutine(GetComponentInChildren<MaterialChange>().FlashWhite());

        StartCoroutine(HitTimePause());
        CheckHealth();
    }
    public void CheckHealth()
    {
        if (Health <= 0)
        {
            StartCoroutine(Death());
        }
    }


    IEnumerator Death()
    {
        //change to death animation
        yield return new WaitForSeconds(.3f);

        Instantiate(_poofParticles, transform.position, transform.rotation);
        Destroy(this.gameObject);

    }

    IEnumerator HitTimePause()
    {
        Time.timeScale = .1f;
        yield return new WaitForSeconds(.04f);
        Time.timeScale = 1;
    }
}
