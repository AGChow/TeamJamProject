using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsController : MonoBehaviour
{
    [SerializeField]
    private Toggle screenshakeToggle;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private AudioMixerGroup musicMixer;
    [SerializeField]
    private AudioMixerGroup sfxMixer;

    private float musicVolume = .5f;
    private float sfxVolume = .5f;

    void Awake()
    {
        // doing this makes it so we don't have to manually assign these functions to each input element in unity
        // each function will be called when the corresponding slider or toggle is changed
        musicSlider.onValueChanged.AddListener(MusicVolume);
        sfxSlider.onValueChanged.AddListener(SFXVolume);
        screenshakeToggle.onValueChanged.AddListener(ToggleScreenshake);
    }

    void Start()
    {
        // Screenshake
        screenshakeToggle.isOn = PlayerPrefs.GetInt("EnableScreenshake", 1) == 1 ? true : false;
        PlayerPrefs.SetInt("EnableScreenshake", screenshakeToggle.isOn ? 1 : 0);
        
        // Music volume
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", .5f);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        musicSlider.value = musicVolume;    

        // SFX volume
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", .5f);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        sfxSlider.value = sfxVolume;
    }

    public void ToggleScreenshake(bool val)
    {
        PlayerPrefs.SetInt("EnableScreenshake", val ? 1 : 0);
    }

    public void MusicVolume(float val)
    {
        // volume sliders shouldn't be linear - this log function is the standard formula for calculating volume based on the 0.0001 - 1 input
        // ^ 0.0001 and not 0 because log(0) is undefined
        musicMixer.audioMixer.SetFloat("Music", Mathf.Log10(val) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", val);
    }

    public void SFXVolume(float val)
    {
        sfxMixer.audioMixer.SetFloat("SFX", Mathf.Log10(val) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", val);
    }

    public void SampleSFX()
    {
        // plays when the user finishes sliding the SFX slider to demonstrate the new volume
        FindObjectOfType<AudioManager>().Play("placeholder");
    }
}
