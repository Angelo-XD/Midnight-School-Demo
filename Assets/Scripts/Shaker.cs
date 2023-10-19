using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] GameObject CamHolder;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CamHolder.GetComponent<Shake>().Shake1();
        }
    }
}
