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
                    //back exposed, shoots projectiles and slams hand
                    break;
                case 3:
                    //covers back, torches come down, shoots projectiles, slams hands
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
