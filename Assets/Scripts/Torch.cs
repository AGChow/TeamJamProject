using UnityEngine;

public class Torch : MonoBehaviour
{
    public ParticleSystem torchLight;
    private PlayerMovement _player;
    private bool _isLit = false;
    public bool isLit {
        get { return _isLit; }
        set {
            if(_isLit == false && value == true) {
                torchLight.Play();
            }
            else if(_isLit == true && value == false) {
                torchLight.Stop();
            }
            _isLit = value;
        }
    }

    void Start() {
        isLit = true;
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
}
