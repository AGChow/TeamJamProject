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
    private Vector3 _moveDirection;
    private Vector3 _lookDirection;
    private Ray _cameraRay;

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
        _moveDirection = new Vector3(_horizontal, 0f, _vertical).normalized;

        if(_moveDirection.magnitude >= 0.1f) {
            characterController.Move(_moveDirection * speed * Time.deltaTime);
        }
    }

    void HandleRotation() {
        _cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if(_ground.Raycast(_cameraRay, out float rayLength)) {
            _lookDirection = _cameraRay.GetPoint(rayLength);
            _transform.LookAt(new Vector3(_lookDirection.x, _transform.position.y, _lookDirection.z));
        }
    }
}
