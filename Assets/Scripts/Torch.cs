using UnityEngine;
using System.Collections;


public class Torch : MonoBehaviour
{
    public ParticleSystem torchLight;
    private PlayerMovement _player;

    [SerializeField]
    private bool _isLit = false;
    public bool timerTorch = false;


    public GameObject fireParticles;

    [SerializeField]
    private float torchTimer = 4;
    public bool isLit {
        get { return _isLit; }
        set {
            //Turn the torch on
            if(_isLit == false && value == true) {
                fireParticles.SetActive(true);
                torchLight.Play();
                if(timerTorch == true)
                {
                    StartCoroutine(TorchTurnOffCounter());
                }
            }
            //Turn the torch off
            else if(_isLit == true && value == false) {
                fireParticles.SetActive(false);
                torchLight.Stop();
            }
            _isLit = value;
        }
    }

    void Start() {
        isLit = false;
        _player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    public void LightTorch() {
        isLit = true;
    }

    public void ExtinguishTorch() {
        isLit = false;
    }

    public void ToggleTorch() {
        isLit = !isLit;
    }

    //trying to turn torch off on timer dependent on bool but not working :/ (Ari)
    IEnumerator TorchTurnOffCounter()
    {
        yield return new WaitForSeconds(torchTimer);
        if (isLit == true)
        {
            ExtinguishTorch();
        }
    }
}
