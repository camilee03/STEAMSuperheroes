using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    [Header("-------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [SerializeField] private SoundData_SO soundBank;

    private AudioClip currentBGM;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
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
        instance.PlaySFX(soundBank.buttonClick);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    } 
}
