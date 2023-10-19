using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PickUp : MonoBehaviour
{
    public AudioClip PickUpAudio;
    public Transform ItemHolder;
    public LayerMask Item;
    public LayerMask Drawer;
    public float ThrowForce;
    public float SmoothSpeed = 3f;
    public TMP_Text Text;

    Rigidbody RB;
    float Range = 4f;

    AudioSource AS;

    Camera Cam;

    GameObject HitObject;

    RaycastHit HitObj;


    bool IsEquipped;
    bool Interacting;
    bool IsInteractingwithdrawer;

    public int EquippedKeyIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
        IsEquipped = false;
        RB = GetComponent<Rigidbody>();
        Text.gameObject.SetActive(false);
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitObj, Range))
        {
            if (HitObj.collider.CompareTag("Item"))
            {
                Interacting = true;
           
            }
            else
            {
                Interacting = false;
            }

            if (HitObj.collider.CompareTag("Drawer"))
            {
                IsInteractingwithdrawer = true;
            }
            else IsInteractingwithdrawer = false;

        }
        else
        {
            Interacting = false;
            IsInteractingwithdrawer = false;
        }
        if(Interacting)
        {
            Text.gameObject.SetActive(true);
            Text.text = HitObj.collider.name;
        }
        else
        {
            Text.text = null;
            Text.gameObject.SetActive(false);
        }
        if (this.GetComponent<IsAlive>().Isalive)
        {
            if (Interacting && Input.GetKeyDown(KeyCode.E) && !IsEquipped)
            {
                Pick();
            }
            else if (Interacting && Input.GetKeyDown(KeyCode.E) && IsEquipped)
            {
                Drop();
                Pick();
            }
            if (Input.GetKeyDown(KeyCode.Q) && IsEquipped)
            {
                Drop();
            }
        }
        else if(this.GetComponent<IsAlive>().Isalive == false)
        {
            if (IsEquipped)
            {
                Drop();
            }
            else return;
        }

        if (IsInteractingwithdrawer && Input.GetKeyDown(KeyCode.E))
        {
            Open();
        }

    }

    void Pick()
    {
        if(Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitObj, Range, Item))
        {
            if (HitObj.collider.CompareTag("Item"))
            {
                AS.clip = PickUpAudio;
                AS.PlayOneShot(PickUpAudio);
                HitObj.collider.gameObject.layer = 15;
                HitObject = HitObj.transform.gameObject;
                HitObj.transform.SetParent(ItemHolder);
                HitObject.GetComponent<Rigidbody>().useGravity = false;
                HitObject.GetComponent<Rigidbody>().isKinematic = true;
                //HitObject.GetComponent<Rigidbody>().velocity = RB.velocity;
                HitObject.GetComponent<Collider>().isTrigger = true;
                HitObj.transform.position = ItemHolder.position;
                HitObj.transform.rotation = ItemHolder.rotation;
                IsEquipped = true;
                if(HitObject.GetComponent<KeysInteraction>())
                {
                    EquippedKeyIndex = HitObject.GetComponent<KeysInteraction>().KeysIndex;
                }
            }
        }
    }
    void Drop()
    {
        HitObject.gameObject.layer = 8;
        HitObject.transform.SetParent(null);
        HitObject.GetComponent<Rigidbody>().useGravity = true;
        HitObject.GetComponent<Rigidbody>().isKinematic = false;
        //HitObject.GetComponent<Rigidbody>().velocity = Vector3.one;
        HitObject.GetComponent<Rigidbody>().AddForce(Cam.transform.forward * ThrowForce * Time.deltaTime , ForceMode.Impulse);
        HitObject.GetComponent<Rigidbody>().AddForce(Cam.transform.up * ThrowForce * Time.deltaTime, ForceMode.Impulse);
        HitObject.GetComponent<Collider>().isTrigger = false;
        IsEquipped = false;
        if(EquippedKeyIndex >=0)
        {
            EquippedKeyIndex = 0;
        }
    }
    void Open()
    {
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitObj, Range, Drawer))
        {
            if (HitObj.collider.CompareTag("Drawer"))
            {
                HitObj.transform.gameObject.GetComponent<Drawer>().Opening();
            }
        }
    }


    



}
