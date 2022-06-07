using UnityEngine;

public class ThrownSword : MonoBehaviour
{
    private bool _isRecalling = false;
    private bool _isSpinning = true;
    private float _rotationSpeed = 900;
    private float _speed = 10;
    private Transform _target;
    private bool _isMoving;

    private void Update()
    {
        if (_isSpinning)
            Spin();
        if (_isMoving)
            UpdatePosition();
    }

    public void SetIsRecalling(bool active)
    {
        _isRecalling = active;
    }

    public void MoveTo(Transform target)
    {
        _target = target;
        _isMoving = true;
        _isSpinning = true;
    }

    private void StopMovement()
    {
        _target = null;
        _isMoving = false;
        _isSpinning = false;
    }

    private void UpdatePosition()
    {
        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target.position, step);
    }

    public void SetIsSpinning(bool active)
    {
        _isSpinning = active;
    }

    private void Spin()
    {
        float currentY = transform.rotation.y;
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        print("Collided with " + other.name);
        if (other.CompareTag("Player"))
            HandlePlayerCollision(other.GetComponent<PlayerAttack>());
        else if (other.CompareTag("Environment"))
            HandleEnvironmentCollision();
        else if (other.CompareTag("Torch"))
            HandleTorchCollision();
        else if (other.CompareTag("Enemy"))
            HandleEnemyCollision();
    }

    private void HandlePlayerCollision(PlayerAttack playerAttackScript)
    {
        if (!_isRecalling) return;
        playerAttackScript.CatchWeapon();
    }

    private void HandleEnvironmentCollision()
    {
        print("hit environment");
        if (!_isRecalling)
            StopMovement();
    }

    private void HandleTorchCollision()
    {
        print("hit torch");
        if (!_isRecalling)
            StopMovement();
    }

    private void HandleEnemyCollision()
    {
        print("hit enemy");
        if (!_isRecalling)
            StopMovement();
    }
}
