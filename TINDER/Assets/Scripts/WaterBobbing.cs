using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBobbing : MonoBehaviour
{
    private float bobDistance;
    private float bobDuration;
    [SerializeField] private bool rotate;
    private float rotationSpeed;

    void Start()
    {
        rotationSpeed = Random.Range(-0.04f, 0.04f);
        bobDistance = Random.Range(0.5f, 0.9f);
        bobDuration = Random.Range(1.8f, 3f);
        LeanTween.moveY(gameObject, transform.position.y - bobDistance, bobDuration).setEaseInOutSine().setLoopPingPong();
    }

    private void Update()
    {
        if (rotate)
        transform.RotateAround(transform.position, Vector3.up, rotationSpeed);
    }
}
