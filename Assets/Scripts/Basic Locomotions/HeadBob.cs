using UnityEngine;
using EZCameraShake;
public class HeadBob : MonoBehaviour
{
    public static HeadBob Instance;
    public Transform CameraHolder;
    public Transform Camera;


    public float bobFrequency = 5f;
    public float bobamplitude = 0.1f;
    [Range(0,1)]public float bobheadsmoothing=0.1f;

    float Walkingtime;
    Vector3 targetcameraposition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.IsWalking == false) Walkingtime = 0;
        else Walkingtime += Time.deltaTime;

        targetcameraposition = CameraHolder.position + CalculateHeadOffset(Walkingtime);

        Camera.position = Vector3.Lerp(Camera.position, targetcameraposition, bobheadsmoothing);

        if((Camera.position-targetcameraposition).magnitude<=0.001)
        {
            Camera.position = targetcameraposition;
        }
    }


    Vector3 CalculateHeadOffset(float t)
    {
        float Horizontal;
        float Vertical;

        Vector3 Offset = Vector3.zero;

        if(t>0)
        {
            Horizontal = Mathf.Cos(t * bobFrequency) * bobamplitude;
            Vertical = Mathf.Sin(t * bobFrequency * 2) * bobamplitude;

            Offset = CameraHolder.right * Horizontal + CameraHolder.up * Vertical;
        }

        return Offset;
    }

 
}
