using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : MonoBehaviour
{
    [SerializeField]
    private bool _isShielded;

    public bool IsShielded() {
        return _isShielded;
    }
    public void IsShielded(bool val) {
        _isShielded = val;
    }
}
