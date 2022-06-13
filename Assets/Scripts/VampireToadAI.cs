using UnityEngine;
using UnityEngine.AI;
public class VampireToadAI : MonoBehaviour
{
    public Torch torch;
    public GameObject eyesGraphics;
    public float speed = 10f;
    public float distanceFromPlayer = 2f;
    public bool nearTorch;


    //animation
    // private Animator anim;

    //target should be set to player in inspector
    [SerializeField] private Transform target;
    private NavMeshAgent _navMeshAgent;

    private Detection detectBools;


    void Awake()
    {
        // anim = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = speed / 3;
        _navMeshAgent.acceleration = speed / 3;
        detectBools = GetComponentInChildren<Detection>();
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



        ///////ALT AGC trying to tie this into a way that allows any torch
        ////behavior when torch is on
        //if (detectBools.torchReaction == true)
        //{
        //    eyesGraphics.SetActive(false);
        //    // anim.SetBool("Awake", false);
        //    _navMeshAgent.destination = this.transform.position;
        //}
        ////behavior when torch is off
        //else
        //{
        //    _navMeshAgent.destination = target.position;
        //    // anim.SetBool("Awake", true);
        //    eyesGraphics.SetActive(true);
        //}

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage(5);
        }
    }
}
