using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk : MonoBehaviour
{
    [Header("Chalk Piece Settings")]
    public Transform EndPoint;
    public GameObject TrailPrefab;
    public TrailRenderer TR;

    bool IsHolded;
    Camera Cam;
    RaycastHit HitPoint;
    private void Start()
    {
        Cam = Camera.main;
        TR.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemHolder"))
        {
            IsHolded = true;
        }
        else IsHolded = false;
    }
    private void Update()
    {
        if (IsHolded)
        {
            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitPoint, 3f))
            {
                EndPoint.position = HitPoint.point;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Draw();
            }
            else if (Input.GetButtonUp("Fire1")) return;
        }
        else return;
    
    }
    void Draw()
    {
        GameObject Prefab = Instantiate(TrailPrefab, EndPoint.position, Quaternion.identity); 
        Prefab.transform.position = Vector3.Slerp(transform.position, EndPoint.position, 1f);
    }
}
