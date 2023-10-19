using UnityEngine;

public class Audio_Trigger : MonoBehaviour
{

    [Header("Play Settings")]
    public bool ConfirmPlay;
    public bool Want_To_Destroy;


    [Header("Audio Settings")]
    public AudioClip Clip;
    


    GameObject CamHolder;
    AudioSource AS;
    

    private void Start()
    {
        CamHolder = GameObject.Find("Camera Holder");
        if (CamHolder != null)
        {
            AS = CamHolder.GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!ConfirmPlay)
        {
            int Rand = Random.Range(0, 5);
            if (other.CompareTag("Player"))
            {
                if (Rand == 2)
                {
                    AS.clip = Clip;
                    AS.PlayOneShot(Clip);
                }
            }
        }
        else if(ConfirmPlay)
        {
            AS.clip = Clip;
            AS.PlayOneShot(Clip);
            if(Want_To_Destroy)
            {
                Destroy(gameObject);
            }
        }
      
    }
}
