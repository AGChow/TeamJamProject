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
    private bool _isPushing;

    void Awake()
    {
        // anim = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = speed / 3;
        _navMeshAgent.acceleration = speed / 3;
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    [System.Obsolete]
    void Update()
    {
        rb.angularVelocity = Vector3.zero;
        if(!_isPushing)
            rb.velocity = Vector3.zero;
        //behavior when torch is on
        if (torch.isLit)
        {
            if(eyesGraphics.activeSelf)
            {
                StartCoroutine(Freeze(true));
            }

            eyesGraphics.SetActive(false);
            // anim.SetBool("Awake", false);
            
            //feedbackresponse to being frozen
            GetComponentInChildren<MaterialChange>().ChangeToAltMaterial();
            GetComponentInChildren<Animator>().SetBool("Frozen", true);
            

            _navMeshAgent.destination = transform.position;
        }
        //behavior when torch is off
        else
        {

            _navMeshAgent.destination = target.position;
            // anim.SetBool("Awake", true);
            GetComponentInChildren<Animator>().SetBool("Frozen", false);

            GetComponentInChildren<MaterialChange>().ChangeBackToOrigingalMaterial();
            eyesGraphics.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !torch.isLit)
            collision.gameObject.GetComponent<Player>().Damage(1);
    }
    public IEnumerator DecreaseVelocity()
    {
        _isPushing = true;
        yield return new WaitForSeconds(.4f);

        StartCoroutine(Freeze());
        _isPushing = false;
    }

    public IEnumerator Freeze(bool wait = false)
    {
        if(wait)
            yield return new WaitForEndOfFrame();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
