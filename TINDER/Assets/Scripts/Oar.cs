using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oar : MonoBehaviour
{
    private Vector3 defaultRot;
    // Start is called before the first frame update
    void Start()
    {
        defaultRot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            Swing();
        }
    }

    public void Swing() {
        GetComponent<Animator>().SetTrigger("Swing");
    }
}
