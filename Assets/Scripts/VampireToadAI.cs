using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class VampireToadAI : MonoBehaviour
{
    public Torch torch;
    public GameObject eyesGraphics;
    public float speed = 10f;
    public Rigidbody rb;


    //animation
    // private Animator anim;

    [SerializeField] private Transform target;
    private NavMeshAgent _navMeshAgent;

    void Awake()
    {
        // anim = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = speed / 3;
        _navMeshAgent.acceleration = speed / 3;
        rb = GetComponent<Rigidbody>();
    }

    [System.Obsolete]
    void Update()
    {
        //behavior when torch is on
        if (torch.isLit)
        {
            eyesGraphics.SetActive(false);
            // anim.SetBool("Awake", false);
            _navMeshAgent.destination = transform.position;
        }
        //behavior when torch is off
        else
        {
            _navMeshAgent.destination = target.position;
            // anim.SetBool("Awake", true);
            eyesGraphics.SetActive(true);
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(!torch.isLit)
                other.GetComponent<Player>().Damage(5);
        }
    }
    public IEnumerator DecreaseVelocity()
    {
        yield return new WaitForSeconds(.5f);
        while(rb.velocity != Vector3.zero)
        {
            rb.velocity = rb.velocity * Mathf.Pow(.99f, 2f) * Time.deltaTime;
        }
    }
}
