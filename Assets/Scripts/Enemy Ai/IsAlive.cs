using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAlive : MonoBehaviour
{
    public bool Isalive;

    private void Start()
    {
        Isalive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name=="Eyes")
        {
            other.GetComponentInParent<EnemyController>().Check();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Eyes")
        {
            other.GetComponentInParent<EnemyController>().Check();
        }
    }

}
