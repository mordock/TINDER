using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    private int currentScene;
    private VideoPlayer videoPlayer;
    private float videoTimer;
    private float pauseTime;
    [SerializeField]
    private float[] timeStamps;
    private bool isPlaying = true;
    private bool canPause = true;
    private float length;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 1)
        {
            videoPlayer = GameObject.FindGameObjectWithTag("Video").GetComponent<VideoPlayer>();
            length = (float)videoPlayer.length;
        }
    }
    void Update()
    {
        Debug.Log(videoTimer);
        if (isPlaying) { videoTimer = (float)videoPlayer.time; }

        foreach (float t in timeStamps)
        {
            if (videoTimer > t && videoTimer < t + 0.5f && canPause)
            {
                videoPlayer.Pause();
                isPlaying = false;
                canPause = false;
            }
        }

        if (Input.anyKeyDown)
        {
            videoPlayer.Play();
            isPlaying = true;
            StartCoroutine(PauseDelay());
        }

        if((float)videoPlayer.time >= length - 0.2f) {
            SceneManager.LoadScene(2);
        }
    }

    private IEnumerator VideoRoutine(float delay)
    {
        yield return new WaitForSeconds((float)delay);
        SceneManager.LoadScene(2);
    }

    private IEnumerator PauseDelay()
    {
        yield return new WaitForSeconds(1);
        canPause = true;
    }
}
