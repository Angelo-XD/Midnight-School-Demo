using UnityEngine;

public class Material_Changer : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] MeshRenderer[] Object;
    [SerializeField] Material Target_Material;
    [SerializeField] bool Want_To_Destroy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(MeshRenderer ms in Object)
            {
                ms.material = Target_Material;
            }
            if(Want_To_Destroy)
            {
                Destroy(gameObject);
            }
        }
    }

}
