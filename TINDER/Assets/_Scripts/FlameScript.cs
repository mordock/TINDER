using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameScript : MonoBehaviour
{
    private GameManager gm;
    private Transform playerPos;
    [SerializeField]
    private Vector3 offset;
    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(gm.middle, playerPos.position.y, playerPos.position.z) + offset;
    }
}
