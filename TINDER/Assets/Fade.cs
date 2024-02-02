using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fade : MonoBehaviour
{ 
public float fadeSpeed = 1.0f;
public float minAlpha = 0.0f;
public float maxAlpha = 1.0f;

private TextMeshProUGUI tmpText;
private bool isFadingOut = false;

void Start()
{
    tmpText = GetComponent<TextMeshProUGUI>();
}

void Update()
{
    Color currentColor = tmpText.color;

    if (!isFadingOut)
    {
        currentColor.a += fadeSpeed * Time.deltaTime;
        if (currentColor.a >= maxAlpha)
        {
            currentColor.a = maxAlpha;
            isFadingOut = true;
        }
    }
    else
    {
        currentColor.a -= fadeSpeed * Time.deltaTime;
        if (currentColor.a <= minAlpha)
        {
            currentColor.a = minAlpha;
            isFadingOut = false;
        }
    }

    tmpText.color = currentColor;
}
}
