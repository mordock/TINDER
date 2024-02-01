using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oar : MonoBehaviour
{
    [SerializeField]
    private Transform oarPoint;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = oarPoint.position;
    }

    public void Swing() {
        GetComponent<Animator>().SetTrigger("Swing");
    }
}
