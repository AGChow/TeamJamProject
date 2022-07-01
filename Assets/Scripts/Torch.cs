using UnityEngine;
using System.Collections;


public class Torch : MonoBehaviour
{
    public ParticleSystem torchLight;
    private PlayerMovement _player;
    [SerializeField]
    private PuzzleManager _puzzleManager;
    public BossRoomManager _bossRoomManager;

    [SerializeField]
    private bool _isLit = false;
    [SerializeField]
    private bool cantBeTurnedOff;
    public bool _isTimerTorch = false;

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
                
                FindObjectOfType<AudioManager>().Play("FireOn");
                
                if(_isTimerTorch == true)
                {
                    StopAllCoroutines();
                    StartCoroutine(TorchTurnOffCounter());
                }
            }
            //Turn the torch off
            else if(_isLit == true && value == false) {
                if(GetComponentInChildren<TorchVampDetection>() && GetComponentInChildren<TorchVampDetection>().frozenVampires.Count > 0) {
                    foreach(VampireToadAI vamp in GetComponentInChildren<TorchVampDetection>().frozenVampires) {
                        vamp.frozen = false;
                    }
                    GetComponentInChildren<TorchVampDetection>().frozenVampires.Clear();
                }
                FindObjectOfType<AudioManager>().Play("FireOff");

                fireParticles.SetActive(false);
                torchLight.Stop();
            }

            _isLit = value;
        }
    }

    void Awake() {
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
        if(cantBeTurnedOff == true)
        {
            return;
        }
        else
        {
            isLit = !isLit;
            //Checks to see if all other torches are lit to complete the puzzles
            if (_puzzleManager != null)
            {
                _puzzleManager.CheckCompleteConditions();
            }
            else if(_bossRoomManager != null)
            {
                //freeze boss when all torches are lit
                _bossRoomManager.CheckCompleteConditions();
            }

        }
    }

    IEnumerator TorchTurnOffCounter()
    {
        // Keeps the torch timer from counting down while the sword is thrown at the torch
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
