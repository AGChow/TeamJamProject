using System.Collections;
using UnityEngine;

public enum WatcherType {
    Door,
    Platform,
    Bridge
}

public class Watcher : MonoBehaviour
{
    public WatcherType watcherType;

    [Header("Platform Settings")]
    public Vector3 moveToLocation;
    public float moveTime = 1f;
    private Vector3 originalLocation;

    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        if(watcherType == WatcherType.Platform)
        {
            originalLocation = _transform.position;
        }
    }

    public void Activate()
    {
        switch(watcherType) 
        {
            case WatcherType.Door:
                ActivateDoor();
                break;
            case WatcherType.Platform:
                ActivatePlatform();
                break;
            case WatcherType.Bridge:
                ActivateBridge();
                break;
            default:
                break;
        }
    }

    public void Deactivate()
    {
        switch(watcherType) 
        {
            case WatcherType.Door:
                DeactivateDoor();
                break;
            case WatcherType.Platform:
                DeactivatePlatform();
                break;
            case WatcherType.Bridge:
                DeactivateBridge();
                break;
            default:
                break;
        }
    }

    // Door handling
    void ActivateDoor()
    {
        // TODO: Replace with open door animation
        GetComponent<Renderer>().enabled = false;
    }
    void DeactivateDoor()
    {
        // TODO: Replace with close door animation
        GetComponent<Renderer>().enabled = true;
    }

    // Platform handling
    void ActivatePlatform()
    {
        StartCoroutine(LerpPosition(moveToLocation, moveTime));
    }

    void DeactivatePlatform()
    {
        StartCoroutine(LerpPosition(originalLocation, moveTime));
    }

    // Bridge handling
    void ActivateBridge()
    {
        // TODO: Animate bridge?
        GetComponent<Renderer>().enabled = true;
    }

    void DeactivateBridge()
    {
        // TODO: Animate bridge?
        GetComponent<Renderer>().enabled = false;
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = _transform.position;
        while (time < duration)
        {
            _transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _transform.position = targetPosition;
    }
}
