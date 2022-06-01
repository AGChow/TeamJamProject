using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 6f;

    private float _horizontal;
    private float _vertical;
    private Camera _mainCamera;
    private Transform _transform;
    private Plane _ground;

    void Awake() {
        _mainCamera = Camera.main;
        _transform = transform;
        _ground = new Plane(Vector3.up, Vector3.zero);
    }

    void Update() {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement() {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(_horizontal, 0f, _vertical).normalized;

        if(direction.magnitude >= 0.1f) {
            characterController.Move(direction * speed * Time.deltaTime);
        }
    }

    void HandleRotation() {
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        float rayLength;

        if(_ground.Raycast(cameraRay, out rayLength)) {
            Vector3 lookAt = cameraRay.GetPoint(rayLength);
            _transform.LookAt(new Vector3(lookAt.x, _transform.position.y, lookAt.z));
        }
    }
}
