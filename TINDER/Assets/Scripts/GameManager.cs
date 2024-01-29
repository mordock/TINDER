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
    
    void Start()
    {
        boostMeter = maxBoost;
        pcb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerBoat>();
    }

    // Update is called once per frame
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
    }
}
