using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    //public int SceneIndex;
    public Transform Pos;

    public GameObject Player;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Player"))
        {
            Player.GetComponent<PlayerMovement>().enabled = false;
            Teleport();
            Player.GetComponent<PlayerMovement>().enabled = true;
        }
       
    }


    void Teleport()
    {
        Player.transform.position = Pos.position;
        Player.transform.rotation = Pos.rotation;
    }
}
