using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject heldWeaponObj;
    public GameObject thrownWeaponObj;
    public Transform throwTarget;
    private ThrownSword _swordScript;
    private PauseMenu _pauseMenu;

    //animation
    private Animator _playerAnimator;

    private bool _hasWeapon = true;
    [SerializeField]
    private float _throwDistance = 17f;

    private float recallFailsafeTimerCurrent = 0;
    private readonly float recallFailsafeTimerWait = 10f;

    private void Awake()
    {
        _playerAnimator = GetComponentInChildren<Animator>();
        _swordScript = thrownWeaponObj.GetComponentInChildren<ThrownSword>();
        _pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (_pauseMenu.IsPaused()) return;
        
        HandleMouseInput();
    }

    void UpdateRecallFailsafe() 
    {
        if (_hasWeapon) return;
        if (recallFailsafeTimerCurrent <= 0) CatchWeapon();
        recallFailsafeTimerCurrent -= Time.deltaTime;
    }

    private void HandleMouseInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (_hasWeapon)
                SwingSword();
            else
                RecallWeapon();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (_hasWeapon)
                ThrowWeapon();
            else
                RecallWeapon();
        }
    }

    private void ThrowWeapon()
    {
        heldWeaponObj.SetActive(false);
        thrownWeaponObj.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Throw");

        _swordScript.SetIsRecalling(false);
        thrownWeaponObj.transform.position = transform.position;

        thrownWeaponObj.GetComponent<ThrownSword>().throwSpeedChange();

        // RaycastAll gets all of the objects in the sword's line of sight. Then, it iterates one by one to determine if it will stop at any of the objects it saw
        List<RaycastHit> hits = new();
        hits = Physics.RaycastAll(transform.position, transform.forward, _throwDistance).ToList();

        if (hits.Count > 0)
        {
            bool foundTarget = false;

            for (int i = 0; i < hits.Count; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider == null) continue;
                if (hit.collider.CompareTag("FloorSwitch") || hit.collider.CompareTag("Breakable")) continue;
                
                throwTarget.transform.position = hit.point;
                foundTarget = true;
                break;
            }

            if(!foundTarget)
                throwTarget.transform.position = transform.position + transform.forward * _throwDistance;
        }
        // If RaycastAll didn't find any objects in the path, it travels for the full throw distance, then stops
        else
        {
            throwTarget.transform.position = transform.position + transform.forward * _throwDistance;
        }

        _swordScript.MoveTo(throwTarget);
        _hasWeapon = false;
        recallFailsafeTimerCurrent = recallFailsafeTimerWait;
    }

    public void RecallWeapon()
    {
        //print("recall!");
        FindObjectOfType<AudioManager>().Play("Throw");

        thrownWeaponObj.GetComponent<ThrownSword>().returnSpeedChange();
        thrownWeaponObj.GetComponent<ThrownSword>().transform.parent = null;
        _swordScript.SetIsRecalling(true);
        _swordScript.MoveTo(transform);
    }

    public void CatchWeapon()
    {
        heldWeaponObj.SetActive(true);
        thrownWeaponObj.SetActive(false);
        _swordScript.SetIsRecalling(false);
        _hasWeapon = true;
    }

    public void SwingSword()
    {
        heldWeaponObj.GetComponent<Sword>().SwordSwing();
    }

    public bool HasWeapon() {
        return _hasWeapon;
    }
}
