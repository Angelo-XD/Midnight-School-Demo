using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{

     GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    void Update()
    {
        transform.LookAt(Player.transform);
    }
}
