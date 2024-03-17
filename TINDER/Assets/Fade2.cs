using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Fade2 : MonoBehaviour
{
    public TextMeshProUGUI myTMPText;
    public float fadeInTime = 2f;

    void Start()
    {
        // Set TMP Text to be invisible initially
        Color startColor = myTMPText.color;
        startColor.a = 0f;
        myTMPText.color = startColor;

        // Start the FadeIn coroutine after 30 seconds
        StartCoroutine(FadeInAfterDelay(30f));
    }

    IEnumerator FadeInAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Gradually increase the alpha to 1 over the fadeInTime
        float timer = 0f;
        while (timer < fadeInTime)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInTime);

            // Update the alpha in the TMP Text color
            Color currentColor = myTMPText.color;
            currentColor.a = alpha;
            myTMPText.color = currentColor;

            yield return null;
        }

        // Ensure the alpha is exactly 1
        Color finalColor = myTMPText.color;
        finalColor.a = 1f;
        myTMPText.color = finalColor;
    }
}