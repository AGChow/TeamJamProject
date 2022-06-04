using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject heldWeapon;
    public GameObject thrownWeapon;

    private bool _hasWeapon = true;
    private bool _isRecalling = false;
    private float _throwDistance = 5f;


    void Update()
    {
        HandleMouseInput();
    }

    public bool IsRecalling() => _isRecalling;

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
        heldWeapon.SetActive(false);
        thrownWeapon.SetActive(true);

        thrownWeapon.transform.position = CalculateThrowDestination();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _throwDistance))
        {
            if (hit.collider != null)
            {
                print("hit something");
                thrownWeapon.transform.position = hit.point;
            }
        }
        else
        {
            print("hit nothing");
            thrownWeapon.transform.position = transform.position + transform.forward * _throwDistance;
        }

        _hasWeapon = false;
    }

    private Vector3 CalculateThrowDestination()
    {
        Vector3 destination = transform.forward * _throwDistance;
        Vector3 destinationWithHeight = new Vector3(destination.x, 2, destination.z);
        return destinationWithHeight;
    }

    private void RecallWeapon()
    {
        print("recall!");
        _isRecalling = true;
        thrownWeapon.transform.position = transform.position;
    }

    public void CatchWeapon()
    {
        heldWeapon.SetActive(true);
        thrownWeapon.SetActive(false);
        _isRecalling = false;
        _hasWeapon = true;
    }
}
