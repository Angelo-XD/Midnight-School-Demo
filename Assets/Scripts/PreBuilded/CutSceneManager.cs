using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
public class CutSceneManager : MonoBehaviour
{

    PlayableDirector PD;
    [SerializeField] PlayableAsset Ps;
    [SerializeField] bool WanttoDestroy;

    private void Start()
    {
        PD = FindObjectOfType<PlayableDirector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PD.playableAsset = Ps;
            PD.Play();
            if (WanttoDestroy)
            {
                Destroy(gameObject);
            }
            else return;
        }
    }
}
