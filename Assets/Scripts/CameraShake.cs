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

    public void ScreenShake()
    {
        if(!CheckScreenshake()) return;

        StartCoroutine(Shake());
    }

    public void ScreenShake(float duration, float amount, float decrease)
    {
        if(!CheckScreenshake()) return;
        
        shakeDuration = duration;
        shakeAmount = amount;
        decreaseFactor = decrease;
        StartCoroutine(Shake());
    }

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

    bool CheckScreenshake()
    {
        if(PlayerPrefs.HasKey("EnableScreenshake"))
            return (PlayerPrefs.GetInt("EnableScreenshake") == 1);

        return true;
    }
}