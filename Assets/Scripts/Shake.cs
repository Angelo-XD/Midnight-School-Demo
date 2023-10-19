using UnityEngine;
using EZCameraShake;
public class Shake : MonoBehaviour
{
    [Header("Shake Settings")]
    public float Magnitude;
    public float Roughness;
    public float Start_Time;
    public float End_Time;


    public void Shake1()
    {
        CameraShaker.Instance.ShakeOnce(Magnitude, Roughness, Start_Time, End_Time);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Shake1();
        }
        
    }
}

