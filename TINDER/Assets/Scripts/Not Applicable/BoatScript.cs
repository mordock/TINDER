using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    [SerializeField]
    private float boatSpeed;
    [SerializeField]
    private Transform[] checkpoints;
    private int checkpointIndex;
    private float t;

    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveBoat();
    }

    private void MoveBoat()
    {
        Vector3 targetPos = new Vector3(checkpoints[checkpointIndex].position.x, transform.position.y, checkpoints[checkpointIndex].position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, boatSpeed * Time.fixedDeltaTime);
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance < 1 && checkpointIndex != checkpoints.Length - 1)
        {
            checkpointIndex += 1;
            t = 0;
        }
        t += Time.fixedDeltaTime * 2;
        if (t > 1) { t = 1; }
        Vector3 viewDir = Vector3.Lerp(transform.position, targetPos, t);
        transform.LookAt(viewDir);
    }
}
