using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject heldWeapon;
    public GameObject thrownWeapon;

    private bool _hasWeapon = true;
    private int _throwDistance = 25;


    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (_hasWeapon)
                ThrowWeapon();
            else
                RecallWeapon();
        }
    }

    private void ThrowWeapon()
    {
        print("throw!");
        heldWeapon.SetActive(false);
        thrownWeapon.SetActive(true);
    }

    private void RecallWeapon()
    {
        print("recall!");
    }

    private void CatchWeapon()
    {
        heldWeapon.SetActive(true);
        thrownWeapon.SetActive(false);
    }
}
