using UnityEngine;

public class SetParent : MonoBehaviour
{
    public Transform Drawer;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            other.transform.SetParent(Drawer);
        }
    }
}
