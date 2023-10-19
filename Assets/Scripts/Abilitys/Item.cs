using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour
{
    public AudioClip Clip;

    AudioSource AS;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
        AS.playOnAwake = false;
        AS.spatialBlend = 1f;
        AS.volume = .5f;

    }
    private void OnCollisionEnter(Collision collision)
    {

        AS.clip = Clip;
        AS.PlayOneShot(Clip);
    }
}
