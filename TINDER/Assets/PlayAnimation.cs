using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public string triggerToCall;

    public void PlayTriggerAnimation() {
        GetComponent<Animator>().SetTrigger(triggerToCall);
    }
}
