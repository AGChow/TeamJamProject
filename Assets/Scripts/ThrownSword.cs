using UnityEngine;

public class ThrownSword : MonoBehaviour
{
    public Transform parentRotationAnchor;

    private bool _isSpinning = true;
    private float _rotationSpeed = 900;

    private void Update()
    {
        if (_isSpinning)
            Spin();
    }

    public void SetIsSpinning(bool active)
    {
        _isSpinning = active;
    }

    private void Spin()
    {
        float currentY = parentRotationAnchor.rotation.y;
        parentRotationAnchor.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            HandlePlayerCollision();
        else if (other.CompareTag("Environment"))
            HandleEnvironmentCollision();
        else if (other.CompareTag("Torch"))
            HandleTorchCollision();
        else if (other.CompareTag("Enemy"))
            HandleEnemyCollision();
    }

    private void HandlePlayerCollision()
    {
        print("hit player");
    }

    private void HandleEnvironmentCollision()
    {
        print("hit environment");
    }

    private void HandleTorchCollision()
    {
        print("hit torch");
    }

    private void HandleEnemyCollision()
    {
        print("hit enemy");
    }
}
