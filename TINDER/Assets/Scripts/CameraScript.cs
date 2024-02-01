using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject player;
    [SerializeField]
    private Vector3 offset;
    private GameManager gm;
    [SerializeField]
    private float zoomSpeed;
    private float currentAngle;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentAngle = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        transform.position = player.transform.position + offset; 

        if(gm.canZoom)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 15, Time.deltaTime * zoomSpeed);
            currentAngle = Mathf.LerpAngle(currentAngle, 19, Time.deltaTime * zoomSpeed);
            transform.eulerAngles = new Vector3(currentAngle, 0, 0);
        }
    }
}
