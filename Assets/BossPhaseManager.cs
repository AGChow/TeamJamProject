using UnityEngine;

public class BossPhaseManager : MonoBehaviour
{
    private BossEvent _bossEvent;

    [SerializeField]
    private int _phase = 1;
    public int phase {
        get { return _phase; }
        set {

            switch(value) {
                case 1:
                    //no torches, back exposed, slams hand to attack player
                    break;
                case 2:
                    //no torches, back exposed, shoots projectiles
                    StartCoroutine(_bossEvent.InitPhase2());
                    break;
                case 3:
                    //torches drop, covers back, shoots and slams
                        _bossEvent.BringDownTorches();
                        _bossEvent.CoverBack();
                        _bossEvent.rateOfShooting = .2f;
                        _bossEvent.shooting = true;
                    break;
            }

            _phase = value;
        }
    }

    void Awake() {
        _bossEvent = GetComponent<BossEvent>();
    }

    public int GetBossPhase() {
        return _phase;
    }

    public void SetBossPhase(int newPhase) {
        phase = newPhase;
    }

    public void ResumePhase() {

        _bossEvent.bossWeakSpot.currentArmor = _bossEvent.bossWeakSpot.maxArmor;
        switch(_phase) {
            case 1:
                //no torches, back exposed, slams hand to attack player
                break;
            case 2:
                //no torches, back exposed, shoots projectiles
                Debug.Log("continue phase 2");
                StartCoroutine(_bossEvent.InitPhase2());
                Debug.Log("continue phase 2");

                break;
            case 3:
                //torches drop, covers back, shoots and slams
                    _bossEvent.BringDownTorches();
                    _bossEvent.CoverBack();
                    _bossEvent.rateOfShooting = .2f;
                    _bossEvent.shooting = true;
                break;

        }
    }

}
