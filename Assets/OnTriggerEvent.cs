using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEvent : MonoBehaviour
{
    public GameObject Player;
    public GameObject Position;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            print("Workeds");
            Player.transform.position = Position.transform.position;
        }
    }
}
