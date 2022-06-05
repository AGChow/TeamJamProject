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
            if(_isLit == false && value == true) {
                
                fireParticles.SetActive(false);
                torchLight.Stop();
            }
            else if(_isLit == true && value == false) {
                fireParticles.SetActive(true);
                torchLight.Play();
                if(timerTorch == true)
                {
                    StartCoroutine(TorchTurnOffCounter());

                }
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





    //(not sure what this code below does -ariana)
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            _player.activeTriggers.Add(gameObject);
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            _player.activeTriggers.Remove(gameObject);
        }
    }



    //trying to turn torch off on timer dependent on bool but not working :/ (Ari)
    IEnumerator TorchTurnOffCounter()
    {
        yield return new WaitForSeconds(torchTimer);
        if (isLit == true)
        {
            ToggleTorch();
        }
    }
}
