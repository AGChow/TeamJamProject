using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 10f;
    public float turnSpeed = 90f;
    public float gravity = 9.8f;

    private float _horizontal;
    private float _vertical;
    private Camera _mainCamera;
    private Transform _transform;
    private Plane _ground;
    private Vector3 _moveDirection;
    private Vector3 _lookDirection;
    private Ray _cameraRay;
    private float vSpeed = 0f; // current vertical velocity
   

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
        if (characterController.isGrounded){
            vSpeed = 0;
        }
        vSpeed -= gravity * Time.deltaTime;
        _moveDirection = new Vector3(_horizontal, vSpeed, _vertical).normalized;
        
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
