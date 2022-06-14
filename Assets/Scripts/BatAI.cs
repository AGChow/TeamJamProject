using UnityEngine;

public class BatAI : MonoBehaviour
{
    //public Transform focalPoint;
    public Torch torch;
    public GameObject eyesGraphics;
    public int numberOfWaypoints = 10;
    public float speed = 10f;
    public float radius = 5f;


    //animation
    private Animator anim;

    private Vector3[] waypoints;
    private int waypointIndex;
    private Vector3 target;
    private Transform _transform;
    private Vector3 _focalPosition;
    private Vector3 _startPosition;
    private Vector3 _tempPosition;
    private Quaternion lookOnLook;
    private Bezier myBezier;
    private float t = 0f;
    private PlayerMovement _player;

    void Awake() {
        anim = GetComponentInChildren<Animator>();
        _transform = transform;
        _focalPosition = _transform.position;    
        _transform.position = new Vector3(_focalPosition.x, _focalPosition.y + 2f, _focalPosition.z);
        _startPosition = _transform.position;
        _player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void Start()
    {
        GenerateRandomWaypoints();
        UpdateDestination();
    }

    void Update()
    {
        if(_player.IsPaused()) return;
        //behavior when torch is off(AGC)
        if(!torch.isLit)
        {
            _transform.position = _startPosition; // TODO: Convert into flying away
            eyesGraphics.SetActive(false);
            anim.SetBool("Awake", false);
            return;
        }
        if(Vector3.Distance(_transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        } 
        //behavior when torch is on(AGC)
        else
        {
            anim.SetBool("Awake", true);
            eyesGraphics.SetActive(true);

            Vector3 vec = myBezier.GetPointAtTime(t);
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookOnLook, speed * Time.deltaTime * 10f);
            _transform.position = vec;

            t += speed / 1000f;
            if(t > 1f)
                t = 0f;
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            other.GetComponent<Player>().Damage(1);
        }
    }

    void UpdateDestination() {
        target = waypoints[waypointIndex];
        lookOnLook = Quaternion.LookRotation(target - _transform.position);
        t = 0f;
        myBezier = new Bezier( _transform.position, Random.insideUnitSphere * 2f, Random.insideUnitSphere * 2f, target );
    }

    void IterateWaypointIndex() {
        waypointIndex++;
        if(waypointIndex == waypoints.Length) {
            waypointIndex = 0;
        }
    }

    void GenerateRandomWaypoints() {
        waypoints = new Vector3[numberOfWaypoints];
        for(int i = 0; i < numberOfWaypoints; i++) {
            float x = Random.Range(_focalPosition.x - radius, _focalPosition.x + radius);
            float y = Random.Range(1.2f, 2f);
            float z = Random.Range(_focalPosition.z - radius, _focalPosition.z + radius);
            waypoints[i] = new Vector3(x, y, z);
        }
    }
}
