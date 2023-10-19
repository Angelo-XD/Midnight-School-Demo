using UnityEngine;

public class Jumpscare_Trigger : MonoBehaviour
{

    public GameObject PeakerFab;


    private void Start()
    {
        PeakerFab.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PeakerFab.SetActive(true);
        }
    }

}
