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
                    _bossEvent.canShoot = true;
                    _bossEvent.shooting = true;
                    break;
                case 3:
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
}
