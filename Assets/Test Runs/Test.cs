using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
   
    public void LoadLevel (int Index)
    {
        SceneManager.LoadScene(Index);
    }
    public void Clear()
    {
        
    }
}
