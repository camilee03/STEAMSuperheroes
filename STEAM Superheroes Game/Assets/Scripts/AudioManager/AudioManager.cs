using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {  get; private set; }

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] private VolumeSettings volumeSettings;

    public SoundData_SO soundBank;

    private BgmTrack currentBGMTrack = BgmTrack.None;
    private AudioClip bgmClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);

        // DEBUGGING
        //if (PlayerPrefs.HasKey("masterVolume")) PlayerPrefs.DeleteKey("masterVolume");
        //if (PlayerPrefs.HasKey("musicVolume")) PlayerPrefs.DeleteKey("musicVolume");
        //if (PlayerPrefs.HasKey("sfxVolume")) PlayerPrefs.DeleteKey("sfxVolume");
    }

    private void Start()
    {
        if(volumeSettings != null) volumeSettings.InitializeVolume();
        SetBGMTrack(BgmTrack.MAINMENU);
    }

    public void SetBGMTrack(BgmTrack bgmTrack)
    {
        if (bgmTrack == currentBGMTrack) return;

        switch (bgmTrack)
        {
            case BgmTrack.PREMINIGAME:
                bgmClip = soundBank.preMinigameBgm;
                break;
            case BgmTrack.MINING:
                bgmClip = soundBank.miningMinigameBgm;
                break;
            case BgmTrack.PROGRAMMING:
                bgmClip = soundBank.programmingMinigameBgm;
                break;
            case BgmTrack.CIRCUIT:
                bgmClip = soundBank.circuitMinigameBgm;
                break;
            case BgmTrack.ART:
                bgmClip = soundBank.artMinigameBgm;
                break;
            case BgmTrack.MATH:
                bgmClip = soundBank.mathMinigameBgm;
                break;
            case BgmTrack.MAINMENU:
                bgmClip = soundBank.mainMenuBgm;
                break;
            default:
                bgmClip = soundBank.mainMenuBgm;
                break;
        }

        currentBGMTrack = bgmTrack;

        musicSource.clip = bgmClip;
        musicSource.Play();
    }

    public void PlayButtonClick()
    {
        Instance.PlaySFX(soundBank.buttonClick);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    } 
}
