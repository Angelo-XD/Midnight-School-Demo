using UnityEngine;

public class Drawer : MonoBehaviour
{
    AudioSource AS;
    Animator Anim;

    [Header("Audio Settings")]
    public AudioClip OpenD;
    public AudioClip CloseD;

    public bool Isopen;


    int Open;
    // Start is called before the first frame update
    public void Start()
    {
        Anim = GetComponent<Animator>();

        AS = GetComponent<AudioSource>();

        Isopen = false;

        Open = Animator.StringToHash("IsOpen");
    }

    public void  Opening()
    {
        Isopen = !Isopen;

        if(Isopen)
        {
            Anim.SetBool(Open, true);
        
            Isopen = true;
        }
        if(!Isopen)
        {
            Anim.SetBool(Open, false);
       
            Isopen = false;
        }
    }

    public void OpenDrawer()
    {
        AS.clip = OpenD;
        AS.PlayOneShot(OpenD);
    }

    public void CloseDrawer()
    {
        AS.clip = CloseD;
        AS.PlayOneShot(CloseD);
    }

}
