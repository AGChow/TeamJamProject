using UnityEngine;
using System.Collections;


public class Torch : MonoBehaviour
{
    public ParticleSystem torchLight;
    private PlayerMovement _player;
    private PuzzleManager _puzzleManager;

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

                //Checks to see if all other torches are lit to complete the puzzles
                if(_puzzleManager)
                {
                    _puzzleManager.CheckCompleteConditions();
                }
                



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
        _puzzleManager = FindObjectOfType<PuzzleManager>();
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

    IEnumerator TorchTurnOffCounter()
    {
        PlayerAttack playerAttack = GameObject.FindObjectOfType<PlayerAttack>();
        while(!playerAttack.HasWeapon()) {
            yield return null;
        }
        yield return new WaitForSeconds(torchTimer);
        if (isLit == true)
        {
            ExtinguishTorch();
        }
    }
}
