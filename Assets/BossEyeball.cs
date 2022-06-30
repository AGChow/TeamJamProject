using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEyeball : MonoBehaviour
{
    private Vector3 openPos;
    public Vector3 closedPos;
    // Start is called before the first frame update
    void Start()
    {
        openPos = transform.localPosition;   
    }


    public void OpenEye()
    {
        Debug.Log("open");
        transform.localPosition = openPos;
    }
    public void CloseEye()
    {
        Debug.Log("closed");

        transform.localPosition = closedPos;
    }
}
