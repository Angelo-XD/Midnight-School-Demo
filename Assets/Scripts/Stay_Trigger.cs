using UnityEngine;

public class Stay_Trigger : MonoBehaviour
{
    public GameObject OBJ;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OBJ.SetActive(true);
        }
        else OBJ.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OBJ.SetActive(false);
        }
        else OBJ.SetActive(true);
    }
}
