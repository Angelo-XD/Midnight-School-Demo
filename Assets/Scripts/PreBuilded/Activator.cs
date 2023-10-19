using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;
public class Activator : MonoBehaviour
{
    public bool WantToDestroy;
    public GameObject Obj;

    void Start()
    {
        Obj.SetActive(false);
    }
    void OnTriggerEnter(Collider Other)
    {
        if(Other.CompareTag("Player"))
        {
            Obj.SetActive(true);
        }
    }
    void OnTriggerExit(Collider Other)
    {
        if(Other.CompareTag("Player"))
        {
            if (WantToDestroy)
            {
                Destroy(gameObject);


































            }
        }
   
    }
   
}
