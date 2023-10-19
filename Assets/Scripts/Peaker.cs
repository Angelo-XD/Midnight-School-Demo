using UnityEngine;

public class Peaker : MonoBehaviour
{
    float Dist;
    GameObject Player;
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    

    private void Update()
    {
        Dist = Vector3.Distance(transform.position, Player.transform.position);
        if(Dist < 50f)
        {
            GetComponent<Animator>().SetBool("Reverse", true);
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

}
