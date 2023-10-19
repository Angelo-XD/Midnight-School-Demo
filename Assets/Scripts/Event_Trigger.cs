using UnityEngine;
using UnityEngine.Events;

public class Event_Trigger : MonoBehaviour
{
    public UnityEvent Event;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("WOrked");
































            Event.Invoke();
        }
    }
}
