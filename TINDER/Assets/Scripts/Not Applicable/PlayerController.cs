using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    [SerializeField]
    private Transform orientation;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private GameObject feet;
    [SerializeField]
    private float rayLength;
    [SerializeField]
    private LayerMask boatLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraRotate();
        Move();
        BoatCheck();
    }

    void Update()
    {
       
    }

    private void CameraRotate()
    {
        Vector3 cameraDirection = transform.position - new Vector3(cam.gameObject.transform.position.x, transform.position.y, cam.gameObject.transform.position.z);
        orientation.forward = cameraDirection.normalized;
        float xDirection = -Input.GetAxis("Horizontal");
        float zDirection = -Input.GetAxis("Vertical");
        Vector3 playerDirection = orientation.forward * zDirection + orientation.right * xDirection;
        if (playerDirection != Vector3.zero)
        {
            transform.forward = Vector3.Lerp(transform.forward, playerDirection.normalized, Time.fixedDeltaTime * rotationSpeed);
        }
    }
    private void Move()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");
        Vector3 moveDirection = orientation.forward * zDirection + orientation.right * xDirection;
        rb.AddForce(moveDirection * moveSpeed * Time.fixedDeltaTime, ForceMode.Force);

        transform.LookAt(orientation);
    }

    private void BoatCheck()
    {
        bool hit = Physics.Raycast(feet.transform.position, Vector3.down, rayLength, boatLayer);
        Debug.DrawLine(feet.transform.position, new Vector3(feet.transform.position.x, feet.transform.position.y - rayLength, feet.transform.position.z), Color.red);
        Debug.Log(hit);
    }
}
