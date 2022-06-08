using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldEnemy : MonoBehaviour
{
    [SerializeField]
    private bool _isShielded;
    private GameObject playerObj;
    [SerializeField]
    private float turnSpeed = 100f;


    private void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        turnTowardsPlayer();
    }


    public bool IsShielded() {
        return _isShielded;
    }
    public void IsShielded(bool val) {
        _isShielded = val;
    }

    public void turnTowardsPlayer()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = playerObj.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = turnSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
