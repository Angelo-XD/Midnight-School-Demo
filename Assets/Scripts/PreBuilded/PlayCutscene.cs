using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
public class PlayCutscene : MonoBehaviour
{
    public TimelineClip Track;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
