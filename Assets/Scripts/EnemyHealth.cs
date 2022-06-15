using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float Health = 1f;
    public GameObject _poofParticles;
    public GameObject _hitParticles;
    public bool dead;

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
        if (Health <= 0 && dead == false)
        {
            StartCoroutine(Death());
        }
    }


    IEnumerator Death()
    {
        dead = true;
        //change to death animation
        //add death sound
        GetComponentInChildren<Animator>().SetTrigger("Death");

        yield return new WaitForSeconds(1.5f);

        Instantiate(_poofParticles, transform.position, transform.rotation);
        Destroy(this.gameObject);

    }

    IEnumerator HitTimePause()
    {
        GetComponent<Collider>().enabled = false;
        Time.timeScale = .1f;
        yield return new WaitForSeconds(.04f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(.3f);
        GetComponent<Collider>().enabled = true;
    }
}
