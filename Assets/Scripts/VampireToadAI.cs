using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class VampireToadAI : MonoBehaviour
{
    public Torch torch;
    public GameObject eyesGraphics;
    public float speed = 10f;
    private Rigidbody rb;
    private bool agro;


    private Vector3 startPos;

    //animation
    // private Animator anim;

    [SerializeField] private Transform target;
    
    private NavMeshAgent _navMeshAgent;
    private bool _isPushing;

    void Awake()
    {
        startPos = transform.position;

        // anim = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = speed / 3;
        _navMeshAgent.acceleration = speed / 3;

        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(Freeze());

        StartCoroutine(Init());
    }

    [System.Obsolete]
    void Update()
    {
        // also helps prevent drifting bugs
        rb.angularVelocity = Vector3.zero;
        CheckDecreaseVelocity();

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
            if(agro == true)
            {

                _navMeshAgent.destination = target.position;
                // anim.SetBool("Awake", true);
                GetComponentInChildren<Animator>().SetBool("Frozen", false);
                GetComponentInChildren<Animator>().SetBool("Walking", true);

                GetComponentInChildren<MaterialChange>().ChangeBackToOrigingalMaterial();
                eyesGraphics.SetActive(true);
            }
            else
            {
                _navMeshAgent.destination = startPos;
                // anim.SetBool("Awake", true);
                GetComponentInChildren<Animator>().SetBool("Frozen", false);

                GetComponentInChildren<MaterialChange>().ChangeBackToOrigingalMaterial();
                eyesGraphics.SetActive(true);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !torch.isLit)
            collision.gameObject.GetComponent<Player>().Damage(1);
    }

    private void CheckDecreaseVelocity() 
    {
        if (!target) return;
        if (Vector3.Distance(transform.position, target.transform.position) < 2) return;
        StartCoroutine(Freeze());
    }

    // stop velocities, this helps fix drifting bugs
    public IEnumerator Freeze(bool wait = false)
    {
        if(wait)
            yield return new WaitForEndOfFrame();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public IEnumerator Init()
    {
        yield return new WaitForSeconds(7f);
        agro = true;
    }
}
