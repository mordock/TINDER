using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBobbing : MonoBehaviour
{
    [SerializeField] private float bobDistance;
    [SerializeField] private float bobDuration;
    private float rotationSpeed;

    void Start()
    {
        LeanTween.moveY(gameObject, transform.position.y - bobDistance, bobDuration).setEaseInOutSine().setLoopPingPong();
        rotationSpeed = Random.Range(-0.04f, 0.04f);
    }

    private void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed);
    }
}
