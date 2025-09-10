using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public string videoFileName;
    [SerializeField] Button button;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject loadMenuCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Globals.Instance.gameLoaded) { mainMenuCanvas.SetActive(true); loadMenuCanvas.SetActive(false); }
        else
        {
            videoPlayer = GetComponent<VideoPlayer>();
            InitializeVideo();
        }
    }

    void InitializeVideo()
    {
        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
            videoPlayer.Pause();

            videoPlayer.frame = 65;
        }
        else { Debug.LogError("No VideoPlayer component found"); }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        button.gameObject.SetActive(false);

        StartCoroutine(WaitForVideoEnd((float)videoPlayer.length-4.5f));
    }

    IEnumerator WaitForVideoEnd(float time)
    {
        yield return new WaitForSeconds(time);

        mainMenuCanvas.SetActive(true);
        loadMenuCanvas.SetActive(false);
        Globals.Instance.gameLoaded = true;
    }
}
