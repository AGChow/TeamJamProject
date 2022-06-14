using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private Toggle screenshakeToggle;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    private float musicVolume;
    private float sfxVolume;

    public void ToggleScreenshake(bool val)
    {
        PlayerPrefs.SetInt("EnableScreenshake", val ? 1 : 0);
    }
}
