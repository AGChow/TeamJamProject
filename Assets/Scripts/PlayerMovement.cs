using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 7f;
    public float turnSpeed = 90f;
    public float gravity = 9.8f;
    public List<GameObject> activeTriggers = new();
    public ParticleSystem dustParticles;

    private float _horizontal;
    private float _vertical;
    private Camera _mainCamera;
    private Transform _transform;
    private Plane _ground;
    private Vector3 _moveDirection;
    private Vector3 _lookDirection;
    private Ray _cameraRay;
    private float vSpeed = 0f; // current vertical velocity
    private bool _isPaused;
    public bool _canMove;
    private PauseMenu _pauseMenu;

    //animation
    private Animator anim;
    [SerializeField]
    private float velocity;
    private Vector3 previousPos;

    void Awake() {
        anim = GetComponentInChildren<Animator>();

        _mainCamera = Camera.main;
        _transform = transform;
        _ground = new Plane(Vector3.up, Vector3.zero);
        _pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
    }

    void Update() {
        if(_pauseMenu.IsPaused()) return;

        if (_canMove)
        {
            HandleInput();
            HandleMovement();
            HandleRotation();

        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        VampireToadAI vamp = hit.gameObject.GetComponent<VampireToadAI>();
        
        // Push stone vampire
        Rigidbody body = hit.collider.attachedRigidbody;
        float pushPower = 4f;

        if(hit.gameObject.CompareTag("Enemy") && vamp && vamp.frozen) {
            if (body == null || body.isKinematic) return;
            if (hit.moveDirection.y < -0.3) return;

            var pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            body.velocity = pushDir * pushPower;
            // StartCoroutine(vamp.DecreaseVelocity());
        }
    }

    void HandleInput() {
        if(activeTriggers.Count > 0) {
            foreach(GameObject trigger in activeTriggers) {
                // Handle swinging at a torch
                if(Input.GetButtonUp("Fire1") || Input.GetButtonUp("Submit")) {
                    if(trigger.CompareTag("Torch")) {
                        Vector3 directionToTorch = (trigger.transform.position - _transform.position).normalized;
                        float dotProd = Vector3.Dot(directionToTorch, _transform.forward);
                        
                        if(dotProd > 0.5f) { // Player is looking in direction of torch
                            trigger.GetComponent<Torch>().ToggleTorch();
                        }
                    }
                }
            }
        }
    }

    void HandleMovement() {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        previousPos = _transform.position;
        if(!characterController) return;
        
        if (characterController.isGrounded){
            vSpeed = 0;
        }
        vSpeed -= gravity * Time.deltaTime;
        _moveDirection = new Vector3(_horizontal, vSpeed, _vertical).normalized;
        
        if(_moveDirection.magnitude >= 0.1f) {
            characterController.Move(_moveDirection * speed * Time.deltaTime);
        }


        //animation logic
        velocity = (_transform.position - previousPos).magnitude / Time.deltaTime;

        if (velocity >= .5f)
        {
            //going to add direction animations eventually(Ari)
            anim.SetBool("Running", true);
            dustParticles.Play();
        }
        else
        {
            anim.SetBool("Running", false);
            dustParticles.Stop();

        }
    }

    void HandleRotation() {
        _cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if(_ground.Raycast(_cameraRay, out float rayLength)) {
            _lookDirection = _cameraRay.GetPoint(rayLength);
            _transform.LookAt(new Vector3(_lookDirection.x, _transform.position.y, _lookDirection.z));
        }
    }

    public void IsPaused(bool val)
    {
        _isPaused = val;
    }

    public bool IsPaused()
    {
        return _isPaused;
    }

    public void Death()
    {
        speed = 0f;
        turnSpeed = 0f;
    }
    public void ResetMovement()
    {
        _canMove = true;
        speed = 7f;
        turnSpeed = 90;
    }

    public void StopMovement()
    {
        _canMove = false;
        speed = 0f;
        turnSpeed = 0;
    }

    public void SceneTransition()
    {
        transform.position = transform.position + new Vector3(0, 0, 5);
        _canMove = false;
        speed = 0f;
        turnSpeed = 0;

    }
}
