using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // How long the object should shake for.
    public float shakeDuration = .3f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;
    
    private Vector3 _originalPos;
    private Transform _transform;

    void Awake()
    {
        _transform = transform;
    }

    // if this is called, default values (or values assigned in Unity inspector) are used
    public void ScreenShake()
    {
        if(!CheckScreenshake()) return;

        StartCoroutine(Shake());
    }

    // if this is called, the values supplied via code are used
    public void ScreenShake(float duration, float amount, float decrease)
    {
        if(!CheckScreenshake()) return;
        
        shakeDuration = duration;
        shakeAmount = amount;
        decreaseFactor = decrease;
        StartCoroutine(Shake());
    }

    // the shake magic
    IEnumerator Shake()
    {
        _originalPos = _transform.position;
        float timer = 0f;

        while(timer < shakeDuration)
        {
            _transform.position = _originalPos + Random.insideUnitSphere * shakeAmount;
            timer += Time.deltaTime * decreaseFactor;
            
            yield return null;
        }

        _transform.position = _originalPos;
    }

    // Only use screenshake if it's enabled in settings
    bool CheckScreenshake()
    {
        if(PlayerPrefs.HasKey("EnableScreenshake"))
            return (PlayerPrefs.GetInt("EnableScreenshake") == 1);

        return true;
    }
}