using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioManagerinstance {  get; private set; }

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] private SoundData_SO soundBank;

    [SerializeField] private VolumeSettings volumeSettings;

    private AudioClip currentBGM;

    private void Awake()
    {
        if (AudioManagerinstance != null && AudioManagerinstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            AudioManagerinstance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        volumeSettings.InitializeVolume();
        SetBGMTrack(BgmTrack.MAINMENU);
    }

    public void SetBGMTrack(BgmTrack bgmTrack)
    {
        switch (bgmTrack)
        {
            case BgmTrack.PREMINIGAME:
                currentBGM = soundBank.preMinigameBgm;
                break;
            case BgmTrack.MINING:
                currentBGM = soundBank.miningMinigameBgm;
                break;
            case BgmTrack.PROGRAMMING:
                currentBGM = soundBank.programmingMinigameBgm;
                break;
            case BgmTrack.CIRCUIT:
                currentBGM = soundBank.circuitMinigameBgm;
                break;
            case BgmTrack.ART:
                currentBGM = soundBank.artMinigameBgm;
                break;
            case BgmTrack.MATH:
                currentBGM = soundBank.mathMinigameBgm;
                break;
            case BgmTrack.MAINMENU:
                currentBGM = soundBank.mainMenuBgm;
                break;
            default:
                currentBGM = soundBank.mainMenuBgm;
                break;
        }

        musicSource.clip = currentBGM;
        musicSource.Play();
    }

    public void PlayButtonClick()
    {
        AudioManagerinstance.PlaySFX(soundBank.buttonClick);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    } 
}
