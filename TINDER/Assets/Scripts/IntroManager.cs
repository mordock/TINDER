using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    private int currentScene;
    private VideoPlayer videoPlayer;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene == 1)
        {
            videoPlayer = GameObject.FindGameObjectWithTag("Video").GetComponent<VideoPlayer>();
            float length = (float)videoPlayer.length;
            StartCoroutine(VideoRoutine(length));
        }
    }
    void Update()
    {
    }

    private IEnumerator VideoRoutine(float delay)
    {
        yield return new WaitForSeconds((float)delay);
        SceneManager.LoadScene(2);
    }
}
