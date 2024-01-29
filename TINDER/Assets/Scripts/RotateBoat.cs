using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBoat : MonoBehaviour
{
    public float targetAngle;
    private float currentAngle;
    [SerializeField]
    private float rotationSpeed;
    private Transform playerref;
    [SerializeField]
    private Vector3 offset;
    void Start()
    {
        playerref = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        currentAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(0, currentAngle, 0);

        transform.position = playerref.position + offset;

        Debug.Log(targetAngle);
    }
}
