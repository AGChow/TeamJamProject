using UnityEngine;

public class ThrownSword : MonoBehaviour
{
    private bool _isRecalling = false;
    private bool _isSpinning = true;
    private float _rotationSpeed = 900;

    // variables to change throw speed Serialized for adjustment and tuning in inspector (Ari)
    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float _throwSpeed = 15;
    [SerializeField]
    private float _returnSpeed = 25f;

    // variable to adjust swordHeight(Ari)
    private float _height = 1.5f;

    // attatched hitParticles;
    [SerializeField]
    private ParticleSystem environmentHitParticles;
    private Transform _transform;
    private Transform _target;
    private bool _isMoving;

    private void Awake()
    {
        _transform = transform;
    }

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
        if(Vector3.Distance(_transform.position, _target.position) <= 1f)
        {
            HandlePlayerCollision(GameObject.FindObjectOfType<PlayerAttack>());
        }
        float step = _speed * Time.deltaTime;
        _transform.position = Vector3.MoveTowards(_transform.position, _target.position + new Vector3(0,_height,0), step);
    }

    public void SetIsSpinning(bool active)
    {
        _isSpinning = active;
    }

    private void Spin()
    {
        float currentY = _transform.rotation.y;
        _transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }

    //Added ways to change speed in two stages for gamefeel(Ari)
    public void throwSpeedChange()
    {
        _speed = _throwSpeed;
    }
    public void returnSpeedChange()
    {
        _speed = _returnSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            HandlePlayerCollision(other.GetComponent<PlayerAttack>());
        else if (other.CompareTag("Environment") || other.CompareTag("Shield"))
            HandleEnvironmentCollision(other.gameObject);
        else if (other.CompareTag("Torch"))
            HandleTorchCollision(other.gameObject);
        else if (other.CompareTag("Enemy"))
            HandleEnemyCollision(other.gameObject);
        else if (other.CompareTag("Breakable"))
            HandleBreakableCollision(other.gameObject);
        else if (other.CompareTag("BounceBack"))
            HandleBounceBackCollision(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>());
        else if (other.CompareTag("BossWeakSpot"))
            HandleBossWeakSpotCollision(other.gameObject);
        else if (other.CompareTag("BossHitBox"))
            HandleBossHitBoxCollision(other.gameObject);

    }

    private void HandlePlayerCollision(PlayerAttack playerAttackScript)
    {
        if (!_isRecalling) return;

        playerAttackScript.CatchWeapon();
    }

    private void HandleEnvironmentCollision(GameObject enviroObj)
    {
        //parent sword to it's collision surface
        transform.parent = enviroObj.transform;
        Instantiate(environmentHitParticles, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("SwordClank");


        if (!_isRecalling)
            StopMovement();
    }

    private void HandleTorchCollision(GameObject torch)
    {
        if (!_isRecalling)
            StopMovement();
        torch.GetComponent<Torch>().ToggleTorch();
    }

    private void HandleEnemyCollision(GameObject enemyHit)
    {
        FindObjectOfType<AudioManager>().Play("placeholder");
        FindObjectOfType<CameraShake>().ScreenShake(.2f, .25f, 1);

        enemyHit.GetComponent<EnemyHealth>().takeDamage();
        if (!_isRecalling)
            StopMovement();
    }

    private void HandleBreakableCollision(GameObject breakableObj)
    {
        if (!_isRecalling)
            StopMovement();
        breakableObj.GetComponent<BreakableObject>().ObjectDestruction();
    }

    private void HandleBounceBackCollision(PlayerAttack playerAttackScript)
    {
        playerAttackScript.RecallWeapon();
    }

    private void HandleBossWeakSpotCollision(GameObject weakSpot)
    {
        weakSpot.GetComponent<BossWeakSpot>().StunHit();
        FindObjectOfType<CameraShake>().ScreenShake(.2f, .15f, 1);

    }
    private void HandleBossHitBoxCollision(GameObject hitbox)
    {
        hitbox.GetComponentInParent<BossEvent>().TakeDamage();
        FindObjectOfType<CameraShake>().ScreenShake(.2f, .15f, 1);

        FindObjectOfType<Player>().GetComponentInChildren<PlayerAttack>().RecallWeapon();

    }
}
