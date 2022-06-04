using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public GameObject heldWeaponObj;
    public GameObject thrownWeaponObj;
    public Transform throwTarget;
    private ThrownSword _swordScript;


    private bool _hasWeapon = true;
    private float _throwDistance = 17f;

    private void Awake()
    {
        _swordScript = thrownWeaponObj.GetComponentInChildren<ThrownSword>();
    }


    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetButtonDown("Fire2"))
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
        _swordScript.SetIsRecalling(false);
        thrownWeaponObj.transform.position = transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _throwDistance))
        {
            if (hit.collider != null)
                throwTarget.transform.position = hit.point;
        }
        else
        {
            throwTarget.transform.position = transform.position + transform.forward * _throwDistance;
        }

        _swordScript.MoveTo(throwTarget);
        _hasWeapon = false;
    }

    private void RecallWeapon()
    {
        print("recall!");
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
}
