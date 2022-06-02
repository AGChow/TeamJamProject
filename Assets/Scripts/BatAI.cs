using UnityEngine;

public class BatAI : MonoBehaviour
{
    public Transform focalPoint;
    public int numberOfWaypoints = 10;
    public float speed = 10f;
    public float radius = 5f;

    private Vector3[] waypoints;
    private int waypointIndex;
    private Vector3 target;
    private Transform _transform;
    private Vector3 _focalPosition;
    private Quaternion lookOnLook;

    void Awake() {
        _transform = transform;
        _transform.position = focalPoint.position;
    }

    void Start() {
        _focalPosition = focalPoint.position;
        GenerateRandomWaypoints();
        UpdateDestination();
    }

    void Update() {
        if(Vector3.Distance(_transform.position, target) < 1) {
            IterateWaypointIndex();
            UpdateDestination();
        } else {
            _transform.rotation = Quaternion.Slerp(_transform.rotation, lookOnLook, speed * Time.deltaTime);
            _transform.position = Vector3.MoveTowards(_transform.position, target, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            //TODO: Damage player on hit
            print("Bat damaged player!");
        }
    }

    void UpdateDestination() {
        target = waypoints[waypointIndex];
        lookOnLook = Quaternion.LookRotation(target - transform.position);
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
            float y = Random.Range(1.5f, 2.5f);
            float z = Random.Range(_focalPosition.z - radius, _focalPosition.z + radius);
            waypoints[i] = new Vector3(x, y, z);
        }
    }
}
