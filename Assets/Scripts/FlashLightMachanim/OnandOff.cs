using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnandOff : MonoBehaviour
{
    public FlashLight flash;
    [SerializeField] GameObject FlashlightButton;
    private void Start()
    {
        FlashlightButton.SetActive(false);
        flash.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ItemHolder"))
        {
            FlashlightButton.SetActive(true);
            flash.enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ItemHolder"))
        {
            FlashlightButton.SetActive(false);
            flash.enabled = false;
        }
    }

}
