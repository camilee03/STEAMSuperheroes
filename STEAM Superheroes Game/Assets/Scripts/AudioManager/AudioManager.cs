using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("-------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------- Audio Clip --------")]
    [Header("General")]
    public AudioClip levelComplete;
    public AudioClip buttonClick;

    [Header("Level Specific")]
    public AudioClip backgroundMusic;

    [Header("Art Minigame")]
    public AudioClip shapeGrabbed;
    public AudioClip shapeReleased;

    [Header("Circuit Minigame")]
    public AudioClip logicGateOptionsOpened;
    public AudioClip logicGateOptionSelected;
    public AudioClip logicGateEndSuccess;
    public AudioClip logicGateEndFailure;

    [Header("Math Minigame")]
    public AudioClip paintSelected;
    public AudioClip paintApplied;

    [Header("Mining Minigame")]
    public AudioClip clickHexagon;
    public AudioClip mineralCollected;

    [Header("Satellite Minigame")]
    public AudioClip satelliteCollected;
    public AudioClip droneMoving;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayButtonClick()
    {
        instance.PlaySFX(buttonClick);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    } 
}
