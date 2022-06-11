using UnityEngine;

public class VampireToadAI : MonoBehaviour
{
    public Torch torch;
    public GameObject eyesGraphics;
    public float speed = 10f;


    //animation
    private Animator anim;
    private Vector3 target;
    private Transform _transform;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        _transform = transform;
    }

    void Start()
    {

    }

    void Update()
    {
        //behavior when torch is on
        if (torch.isLit)
        {
            eyesGraphics.SetActive(false);
            anim.SetBool("Awake", false);
            return;
        }
        //behavior when torch is off
        else
        {
            anim.SetBool("Awake", true);
            eyesGraphics.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage(5);
        }
    }
}
