using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float anyKeyTime;
    public GameObject anyKey;

    private float currentTime;

    private void Start() {
        anyKey.SetActive(false);
    }

    void Update()
    {
        // Move the camera up based on the input or a constant speed
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        // Alternatively, you can use the following if you want to move based on input
        // float verticalInput = Input.GetAxis("Vertical");
        // transform.Translate(Vector3.up * verticalInput * moveSpeed * Time.deltaTime);


        currentTime += Time.deltaTime;
        if(currentTime > anyKeyTime) {
            anyKey.SetActive(true);
            if (Input.anyKeyDown) {
                SceneManager.LoadScene(0);
            }
        }

        
    }
}
