using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControllerBoat : MonoBehaviour
{
    public float idleSpeed;
    private Rigidbody rb;
    [SerializeField]
    private float rowForce;
    public float maxSpeed;
    [HideInInspector]
    public float currentSpeed;
    [SerializeField, Range(0, 1)]
    float rowIntensity;
    private bool canRowL = true;
    private bool canRowR = true;
    [SerializeField]
    private float rowDelay;
    [SerializeField]
    private float counterForce;
    private const string ramp = "Ramp";
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private Transform transformGC;
    [SerializeField]
    private float rayLength;
    [SerializeField]
    private LayerMask groundLayer;
    [HideInInspector]
    public bool isGrounded = true;
    [SerializeField]
    private float rotationForce;
    private Quaternion targetRotation;
    private float boostMultiplier = 1;
    [HideInInspector]
    public bool isBoosting;
    [HideInInspector]
    public bool canBoost = true;
    private const string obstacle = "Obstacle";
    [SerializeField]
    private float speedLerp;
    private RotateBoat boat;
    private GameManager gm;
    [SerializeField]
    private Oar[] oars;
    private const string checkpoint = "Checkpoint";
    [SerializeField]
    private float speedIncreaseCP;
    [HideInInspector]
    public bool canInput = true;
    private AudioManager am;
    public float maxClampSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
        currentSpeed = maxSpeed;
        boat = GameObject.FindGameObjectWithTag("Boat").GetComponent<RotateBoat>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        am = GameObject.FindGameObjectWithTag("am").GetComponent<AudioManager>();
    }
    void FixedUpdate()
    {
        MoveBoat();
        if (!isGrounded) { LerpRotation(); }
    }

    void Update()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxClampSpeed);
        GroundCheck();
        if (canInput)
        {
            if (isGrounded) { Row(); }
            HandleBoost();
        }
        LerpSpeed();
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag(ramp))
        {
            rb.AddForce(Vector3.forward * jumpForce);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(obstacle) && !gm.isHit)
        {
            currentSpeed = currentSpeed / 2;
            gm.StartHitstunRoutine();
        }

        if (col.gameObject.CompareTag(checkpoint))
        {
            maxSpeed += speedIncreaseCP;
        }
    }
    private void MoveBoat()
    {
        rb.AddForce(Vector3.forward * idleSpeed * Time.fixedDeltaTime * boostMultiplier);
        Vector3 slowDir = -rb.velocity.normalized;
        rb.AddForce(slowDir * Mathf.Lerp(100, idleSpeed, rb.velocity.magnitude / currentSpeed) * Time.fixedDeltaTime * counterForce);
    }

    private void Row()
    {
        if (Input.GetKeyDown(KeyCode.A) && canRowL)
        {
            Vector3 rowDir = new Vector3(rowIntensity, 0, 1);
            rb.AddForce(rowDir * rowForce, ForceMode.Impulse);
            if (boat.targetAngle < 30) { boat.targetAngle += 10; }
            oars[0].Swing();
            //am.PlaySplash();
            StartCoroutine(rowCD(true));
        }
        if (Input.GetKeyDown(KeyCode.D) && canRowR)
        {
            Vector3 rowDir = new Vector3(-rowIntensity, 0, 1);
            rb.AddForce(rowDir * rowForce, ForceMode.Impulse);
            if (boat.targetAngle > -30) { boat.targetAngle -= 10; }
            oars[1].Swing();
            //am.PlaySplash();
            StartCoroutine(rowCD(false));
        }
    }

    private IEnumerator rowCD(bool isLeft)
    {
        if (isLeft)
        {
            canRowL = false;
        }
        if (!isLeft)
        {
            canRowR = false;
        }
        yield return new WaitForSeconds(rowDelay);
        if (isLeft)
        {     
            canRowL = true;
        }
        if (!isLeft)
        {
            canRowR = true;
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayer);
    }

    private void LerpRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationForce * Time.fixedDeltaTime);
    }

    private void RotateBack()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(0, 0, 0, 0), rotationForce/2 * Time.fixedDeltaTime);
    }

    private void HandleBoost()
    {
        if (Input.GetKey(KeyCode.Space) && canBoost)
        {
            boostMultiplier = 2f;
            isBoosting = true;
        }
        if (!canBoost)
        {
            boostMultiplier = 1;
            isBoosting = false;
        }
    }
    private void LerpSpeed()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, speedLerp * Time.deltaTime);
    }
}
