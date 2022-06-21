using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera _introCam;
    public CinemachineVirtualCamera _intro2Cam;
    private float transition1 = 0.5f;
    private float transition2 = 2.5f;
    private float transition3 = 3f;

    public CinemachineVirtualCamera _topDownCam;


    public CinemachineVirtualCamera _exitCam;
    public CinemachineVirtualCamera _exitCam2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroScene());
    }


    IEnumerator IntroScene()
    {
        _topDownCam.Priority = 10;
        _introCam.Priority = 11;
        _intro2Cam.Priority = 10;
        _exitCam.Priority = 10;
        _exitCam2.Priority = 10;
        yield return new WaitForSeconds(.5f);
        _introCam.Priority = 10;
        _intro2Cam.Priority = 11;
        yield return new WaitForSeconds(2.5f);
        _topDownCam.Priority = 11;
        _introCam.Priority = 10;
        _intro2Cam.Priority = 10;

        //change priority
    }

    public IEnumerator ExitScene()
    {

        _topDownCam.Priority = 10;
        _exitCam.Priority = 11;
        _exitCam2.Priority = 10;
        yield return new WaitForSeconds(.5f);
        _topDownCam.Priority = 10;
        _exitCam.Priority = 11;
        _exitCam2.Priority = 10;
        yield return new WaitForSeconds(1.3f);
        _topDownCam.Priority = 10;
        _exitCam.Priority = 10;
        _exitCam2.Priority = 11;
    }
}
