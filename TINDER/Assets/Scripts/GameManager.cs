using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float distort;
    private float chromatic;


    void Start()
    {
        boostMeter = maxBoost;
        pcb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerBoat>();
    }

    void Update()
    {
        if (boostMeter < maxBoost && !pcb.isBoosting)
        {
            boostMeter += chargeSpeed * Time.deltaTime;
        }
        if(boostMeter > 0 && pcb.isBoosting)
        {
            boostMeter -= chargeSpeed * 2 * Time.deltaTime;
        }
        if(boostMeter > 0.1) { pcb.canBoost = true; }
        else { pcb.canBoost = false; }

        PostEffect();
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

    private void PostEffect()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            distort = distortIntensity;
            chromatic = chromaticIntensity; 
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            distort = 0;
            chromatic = 0;
        }
        postMat.SetFloat("_DistortIntensity", distort);
        postMat.SetFloat("_ChromaticIntensity", chromatic);
    }
}
