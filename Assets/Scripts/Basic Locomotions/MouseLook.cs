using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
public class MouseLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    public Transform player;
    [SerializeField] Slider Mouse_slider;
    public float mousesensitivity;
    float xrotation = 0f;

    [SerializeField] FixedTouchField FTF;
    Vector2 LookAxis;
 

    // Start is called before the first frame update
    void Start()
    {
       
        Mouse_slider.value = Mouse_slider.maxValue / 2;
    }

    // Update is called once per frame
    void Update()
    {
        LookAxis = FTF.TouchDist;
        mousesensitivity = Mouse_slider.value;
  
        float mousex = LookAxis.x*mousesensitivity;
        float mousey =LookAxis.y *mousesensitivity;


       
        xrotation -= mousey* Time.deltaTime;
        xrotation = Mathf.Clamp(xrotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
        player.Rotate(Vector3.up * mousex *Time.deltaTime);
     
    }
  

}
