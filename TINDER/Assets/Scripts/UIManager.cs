using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI instructionBegin;
    public TextMeshProUGUI instructionEnd;
    void Start()
    {
        instructionBegin.enabled = false;
        instructionEnd.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
