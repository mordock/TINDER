using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    void Update()
    {
        // Move the camera up based on the input or a constant speed
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // Alternatively, you can use the following if you want to move based on input
        // float verticalInput = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.up * verticalInput * moveSpeed * Time.deltaTime);
      
    }
}
