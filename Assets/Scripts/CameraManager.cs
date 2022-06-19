using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera _introCam;
    public CinemachineVirtualCamera _intro2Cam;


    public CinemachineVirtualCamera _topDownCam;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IntroScene()
    {
        _topDownCam.Priority = 10;
        _introCam.Priority = 11;
        _intro2Cam.Priority = 10;
        yield return new WaitForSeconds(.5f);
        _introCam.Priority = 10;
        _intro2Cam.Priority = 11;
        yield return new WaitForSeconds(2.5f);
        _topDownCam.Priority = 11;
        _introCam.Priority = 10;
        _intro2Cam.Priority = 10;

        //change priority
    }
}
