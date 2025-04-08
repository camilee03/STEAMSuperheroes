using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VidPlayer : MonoBehaviour
{
    VideoPlayer videoPlayer;
    public string videoFileName;
    [SerializeField] Button button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        InitializeVideo();
    }

    void InitializeVideo()
    {
        if (videoPlayer)
        {
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
            videoPlayer.url = videoPath;
            videoPlayer.Play();
            videoPlayer.Pause();

            videoPlayer.frame = 50;
        }
        else { Debug.LogError("No VideoPlayer component found"); }
    }

    public void PlayVideo()
    {
        videoPlayer.Play();
        button.gameObject.SetActive(false);

        StartCoroutine(WaitForVideoEnd((float)videoPlayer.length-3));
    }

    IEnumerator WaitForVideoEnd(float time)
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(2);
    }
}
