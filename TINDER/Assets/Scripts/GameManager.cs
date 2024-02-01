using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float maxBoost;
    private float boostMeter;
    [SerializeField]
    private float chargeSpeed;
    private PlayerControllerBoat pcb;
    [SerializeField]
    private MeshRenderer[] boatRenderers;
    [SerializeField]
    private float stunDelay;
    private bool OnOff;
    [HideInInspector]
    public bool isHit;
    [SerializeField]
    private Material postMat;
    [SerializeField]
    private float distortIntensity;
    [SerializeField]
    private float chromaticIntensity;
    [SerializeField]
    private GameObject dirLight;
    private float currentAngle = -100;
    [SerializeField]
    private float targetAngle;
    [SerializeField]
    private float lightLerpSpeed;
    private float timer;
    [SerializeField]
    private float[] timeStamps;
    private float fadeIntensity = 1;
    private float fadeTarget;
    private float saturationIntensity;
    private float saturationTarget;
    private float glitchFrequency;
    private bool canStartGlitchLoop = true;
    private AudioManager am;
    private UIManager um;
    private bool canStartIR = true;

    void Start()
    {
        boostMeter = maxBoost;
        pcb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerBoat>();
        postMat.SetFloat("_FadeIntensity", fadeIntensity);
        fadeTarget = fadeIntensity;
        saturationTarget = 1.5f;
        am = GameObject.FindGameObjectWithTag("am").GetComponent<AudioManager>();
        um = GameObject.FindGameObjectWithTag("um").GetComponent<UIManager>();
    }

    void Update()
    {
        TimeLine();
        HandleBoost();
        LerpFade();
        LerpSaturation();
    }
    public void StartHitstunRoutine()
    {
        StartCoroutine(Hitstun());
    }
    private IEnumerator Hitstun()
    {
        isHit = true;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(stunDelay);
            foreach (MeshRenderer mr in boatRenderers)
            {
                mr.enabled = OnOff;
            }
            if (!OnOff) { OnOff = true; }
            else { OnOff = false; }
        }
        isHit = false;
    }

    private void HandleBoost()
    {
        if (boostMeter < maxBoost && !pcb.isBoosting)
        {
            boostMeter += chargeSpeed * Time.deltaTime;
        }
        if (boostMeter > 0 && pcb.isBoosting)
        {
            boostMeter -= chargeSpeed * 2 * Time.deltaTime;
        }
        if (boostMeter > 0.1) { pcb.canBoost = true; }
        else { pcb.canBoost = false; }
    }
    private void TimeLine()
    {
        timer = Time.time;
        if (timer > timeStamps[0])
        {
            fadeTarget = 0.97f;
        }
        if (timer > timeStamps[1])
        {
            fadeTarget = -0.5f;
            LerpDirLight();
            if (canStartIR)
            {
                StartCoroutine(InstructionRoutine());
                canStartIR = false;
            }
        }
        if (timer > timeStamps[2])
        {
            if (canStartGlitchLoop)
            {
                glitchFrequency = 0.5f;
                StartCoroutine(Glitch());
                canStartGlitchLoop = false;
            }
        }
        if (timer > timeStamps[3])
        {
            glitchFrequency = 0.5f;
        }
    }

    private IEnumerator Glitch()
    {
        yield return new WaitForSeconds(glitchFrequency);
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            postMat.SetFloat("_DistortIntensity", distortIntensity);
            postMat.SetFloat("_ChromaticIntensity", chromaticIntensity);
            am.SetPitch(-2);
        }
        yield return new WaitForSeconds(0.2f);
        if (i == 0)
        {
            postMat.SetFloat("_DistortIntensity", 0);
            postMat.SetFloat("_ChromaticIntensity", 0);
            am.SetPitch(1);
        }
        StartCoroutine(Glitch());
    }
    private void LerpDirLight()
    {
        if(currentAngle < 60)
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * lightLerpSpeed);
        else
        {
            currentAngle = 60;
        }
        dirLight.transform.eulerAngles = new Vector3(currentAngle, 0, 0);
    }

    private void LerpFade()
    {

        fadeIntensity = Mathf.Lerp(fadeIntensity, fadeTarget, Time.deltaTime * lightLerpSpeed);
        fadeIntensity = Mathf.Clamp(fadeIntensity, 0, 1);
        postMat.SetFloat("_FadeIntensity", fadeIntensity);
    }

    private void LerpSaturation()
    {
        saturationIntensity = Mathf.Lerp(saturationIntensity, saturationTarget, Time.deltaTime * lightLerpSpeed * 0.2f);
        saturationIntensity = Mathf.Clamp(saturationIntensity, 0, 1);
        postMat.SetFloat("_SaturationIntensity", saturationIntensity);
    }

    private IEnumerator InstructionRoutine()
    {
        yield return new WaitForSeconds(6);
        um.instruction.enabled = true;
        yield return new WaitForSeconds(7);
        um.instruction.enabled = false;
    }
}
