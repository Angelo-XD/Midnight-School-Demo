using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Door : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip OpenClip;
    public AudioClip CloseClip;
    public AudioClip LockedClip;
    public AudioClip UnlockedClip;
    public AudioClip RequiedKeyAudio;

  
 

    bool IsOpened;

    Animator Anim;
    AudioSource AS;
    AudioSource PAS;

    [Header("Ui Settings")]
    public GameObject RequriedKeyText;
    public GameObject KeyText;
    public GameObject UnlockedText;
    public int DoorIndex = -1;

    [Header("Door Settings")]
    public bool Locked = false;
    [SerializeField] bool Do_Event;
    [SerializeField] GameObject Event_Object;


    int Open;
    private void Start()
    {
        PAS = GameObject.Find("Camera Holder").GetComponent<AudioSource>();
        Anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();

        IsOpened = false;

        Open = Animator.StringToHash("IsOpen");

        if(Event_Object!=null)
        {
            Event_Object.SetActive(false);
        }
      
    }

    public void FrontOpen()
    {
        IsOpened = !IsOpened;

        if (IsOpened)
        {
            AS.clip = OpenClip;
            AS.PlayOneShot(OpenClip);
            Anim.SetBool(Open, true);
            IsOpened = true;
            Locked = false;
        }
        else if (!IsOpened)
        {
            StartCoroutine(CloseSound());
            Anim.SetBool(Open, false);
        }
    }

    public void Close()
    {
        Anim.SetBool(Open, false);
    }
    public void IsLocked()
    {
        AS.clip = LockedClip;
        Anim.SetTrigger("IsActive");
    }
    public void Deactivate()
    {
        Anim.ResetTrigger("IsActive");
    }
    public void Deactivate1()
    {
        Anim.ResetTrigger("Unlocked");
    }
    public IEnumerator CloseSound()
    {
        yield return new WaitForSeconds(.8f);
        AS.clip = CloseClip;
        AS.PlayOneShot(CloseClip);
    }
    public void Unlocked()
    {
        AS.clip = UnlockedClip;
        AS.PlayOneShot(UnlockedClip);
        Anim.SetTrigger("Unlocked");
        Locked = false;
    }

    public void PlayAudio()
    {
        AS.clip = LockedClip;
        AS.PlayOneShot(LockedClip);
        int Rand = Random.Range(0, 4);
        if(Rand==2)
        {
            PAS.clip = RequiedKeyAudio;
            PAS.PlayOneShot(RequiedKeyAudio);
        }
        if (Event_Object != null && Do_Event) 
        {
            Event_Object.SetActive(true);
        }
    }
}
