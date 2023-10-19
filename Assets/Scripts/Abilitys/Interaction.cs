using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Interaction : MonoBehaviour
{


    [Header("Notes Settings")]
    public GameObject Notes;
    public TMP_Text NoteText;
    [SerializeField] GameObject PlayerJoystick;
    [SerializeField] GameObject PauseButton;

    [Header("Pick Up & Drop Settings")]
    [SerializeField] GameObject ItemParent;
    public float ThrowForce;
    public float SmoothSpeed = 3f;
    public GameObject ItemText;
    float Range = 4f;


    [Header("UI Settings")]
    [SerializeField] Button PickUPButton;
    [SerializeField] Button DoorButton;
    [SerializeField] Button DrawerButton;
    [SerializeField] Button NotesButton;
    [SerializeField] Button NotesBackButton;
    [SerializeField] Button DropButton;

    [Header("Audio Settings")]
    public AudioClip Breath;
    public AudioClip PickUpAudio;

    [Header("Public Transforms")]
    public Transform ItemHolder;






    //Getting Some Components
    
    [SerializeField] AudioSource PAS;
    Camera Cam;
    GameObject HitObject;

    //Ray
    RaycastHit HitObj;

    //Bools
    bool IsEquipped;
    bool InteractingWithItem;
    bool IsInteractingwithdrawer;
    bool IsInteractingWithDoor;
    bool IsInteractingWithNote;
    bool OnReading;

    [Header("Key Settings")]
    public int EquippedKeyIndex = 0;

    void Start()
    {
        //Initializing
        PickUPButton.gameObject.SetActive(false);
        DoorButton.gameObject.SetActive(false);
        DrawerButton.gameObject.SetActive(false);
        NotesButton.gameObject.SetActive(false);
        NotesBackButton.gameObject.SetActive(false);
        DropButton.gameObject.SetActive(false);
        Cam = Camera.main;
        IsEquipped = false;
        ItemText.gameObject.SetActive(false);
      
        Notes.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //checking for interaction
        #region Bool Manager
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitObj, Range))
        {
            if (HitObj.collider.CompareTag("Item") || HitObj.collider.CompareTag("CutSceneItem"))
            {
                PickUPButton.gameObject.SetActive(true);
                InteractingWithItem = true;
            }
            else
            {
                PickUPButton.gameObject.SetActive(false);
                InteractingWithItem = false;
            }

            if (HitObj.collider.CompareTag("Drawer"))
            {
                DrawerButton.gameObject.SetActive(true);
                IsInteractingwithdrawer = true;
            }
            else
            {
                IsInteractingwithdrawer = false;
                DrawerButton.gameObject.SetActive(false);
            }
            if (HitObj.collider.CompareTag("Door"))
            {
                DoorButton.gameObject.SetActive(true);
                IsInteractingWithDoor = true;
            }
            else
            {
                IsInteractingWithDoor = false;
                DoorButton.gameObject.SetActive(false);
            }
            if (HitObj.collider.CompareTag("Notes"))
            {
                NotesButton.gameObject.SetActive(true);
                IsInteractingWithNote = true;
            }
            else
            {
                NotesButton.gameObject.SetActive(false);
                IsInteractingWithNote = false;
            }

        }
        else
        {
            DoorButton.gameObject.SetActive(false); DrawerButton.gameObject.SetActive(false); PickUPButton.gameObject.SetActive(false); NotesButton.gameObject.SetActive(false);
            InteractingWithItem = false;
            IsInteractingwithdrawer = false;
            IsInteractingWithDoor = false;
            IsInteractingWithNote = false;
        }


        #endregion

        #region Checking to show the text

        if (InteractingWithItem)
        {
            ItemText.SetActive(true);
            ItemText.gameObject.GetComponent<TMP_Text>().text = HitObj.collider.name;
        }
        else
        {
            ItemText.gameObject.GetComponent<TMP_Text>().text = null;
            ItemText.SetActive(false);
        }


        #endregion
        if (IsEquipped)
        {
            DropButton.gameObject.SetActive(true);
        } else DropButton.gameObject.SetActive(false);
        if (OnReading)
        {
            NotesButton.gameObject.SetActive(false);
        }



        #region Drawer
        if (IsInteractingwithdrawer && Input.GetKeyDown(KeyCode.E))
        {
            OpenDrawer();
        }
        #endregion

        #region Door
        if (IsInteractingWithDoor && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
        #endregion

        #region Notes
        if (IsInteractingWithNote && Input.GetKeyDown(KeyCode.E))
        {
            NotesInteraction();
        }
        if (OnReading && Input.GetButtonDown("Cancel"))
        {
            Back();
        }
        #endregion
    }

    public void Pick()
    {
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitObj, Range))
        {

            if (HitObj.collider.CompareTag("Item"))
            {
                if (HitObj.collider.GetComponent<Item_Trigger>())
                {
                    if(HitObj.collider.GetComponent<Item_Trigger>().isActiveAndEnabled)
                    {
                        HitObj.collider.GetComponent<Item_Trigger>().CutSceneObject.SetActive(true);
                        HitObj.collider.GetComponent<Item_Trigger>().enabled = false;
                        if (HitObj.collider.GetComponent<Item_Trigger>().DiasbleCollider)
                        {
                            HitObj.collider.GetComponent<Collider>().enabled = false;
                        }
                        if (HitObj.collider.GetComponent<Item_Trigger>().IsEquipable)
                        {
                            PickProPerties();
                        }
                        return;
                    }
                }
                

                    PickProPerties();
                
  
            }
            else DropButton.gameObject.SetActive(false);
     

        }
    }
    public void Drop()
    {
        HitObject.GetComponent<Collider>().isTrigger = false;
        HitObject.gameObject.layer = 8;
        HitObject.transform.SetParent(ItemParent.transform);
        HitObject.GetComponent<Rigidbody>().useGravity = true;
        HitObject.GetComponent<Rigidbody>().isKinematic = false;
        HitObject.GetComponent<Rigidbody>().AddForce(Cam.transform.forward * ThrowForce * Time.deltaTime, ForceMode.Impulse);
        HitObject.GetComponent<Rigidbody>().AddForce(Cam.transform.up * ThrowForce * Time.deltaTime, ForceMode.Impulse);
        DropButton.gameObject.SetActive(false);
        IsEquipped = false;
        if (EquippedKeyIndex >= 0)
        {
            EquippedKeyIndex = 0;
        }
        HitObject = null;
    }
    public void PickProPerties()
    {
        PAS.clip = PickUpAudio;
        PAS.PlayOneShot(PickUpAudio);
        HitObj.collider.gameObject.layer = 15;
        HitObject = HitObj.transform.gameObject;
        HitObj.transform.SetParent(ItemHolder);
        HitObject.GetComponent<Rigidbody>().useGravity = false;
        HitObject.GetComponent<Rigidbody>().isKinematic = true;
        HitObject.GetComponent<Collider>().isTrigger = true;
        HitObj.transform.position = ItemHolder.position;
        HitObj.transform.rotation = ItemHolder.rotation;
        IsEquipped = true;
        DropButton.gameObject.SetActive(true);
        if (HitObject.GetComponent<KeysInteraction>())
        {
            EquippedKeyIndex = HitObject.GetComponent<KeysInteraction>().KeysIndex;
        }
    }
    #region Drawer Interaction
    public void OpenDrawer()
    {
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitObj, Range))
        {
            if (HitObj.collider.CompareTag("Drawer"))
            {
                HitObj.transform.gameObject.GetComponent<Drawer>().Opening();
            }
        }
    }
    #endregion

    #region DoorInteraction
    public void OpenDoor()
    {
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitObj, Range))
        {
            if (HitObj.collider.CompareTag("Door"))
            {
                if (HitObj.collider.GetComponent<Door>().Locked == false)
                {
                    HitObj.collider.GetComponent<Door>().FrontOpen();
                }
                if (HitObj.collider.GetComponent<Door>().Locked)
                {
                    if (EquippedKeyIndex == HitObj.collider.GetComponent<Door>().DoorIndex)
                    {
                        HitObj.collider.GetComponent<Door>().Unlocked();
                    }
                    else if (EquippedKeyIndex != HitObj.collider.GetComponent<Door>().DoorIndex)
                    {

                        HitObj.collider.GetComponent<Door>().IsLocked();
                    }
                }
            }
        }
    }
    #endregion

    public void NotesInteraction()
    {
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out HitObj, Range))
        {
            if (HitObj.collider.CompareTag("Notes"))
            {
                PlayerJoystick.SetActive(false);
                PauseButton.SetActive(false);
                NotesBackButton.gameObject.SetActive(true);
                Notes.SetActive(true);
                NoteText.text = HitObj.collider.GetComponent<Note>().Text;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponentInChildren<HeadBob>().enabled = false;
                FindObjectOfType<MouseLook>().enabled = false;
                FindObjectOfType<PauseMenu>().enabled = false;
                OnReading = true;
                
            }
            else
            {
                PlayerJoystick.SetActive(true);
                PauseButton.SetActive(true);
                Notes.SetActive(false);
                NoteText.text = null;
                GetComponent<PlayerMovement>().enabled = true;
                GetComponentInChildren<HeadBob>().enabled = true;
                FindObjectOfType<MouseLook>().enabled = true;
                FindObjectOfType<PauseMenu>().enabled = true;
                OnReading = false;
                NotesBackButton.gameObject.SetActive(false);
            }
        }
    }
    public void Back()
    {
        PlayerJoystick.SetActive(true);
        PauseButton.SetActive(true);
        Notes.SetActive(false);
        NoteText.text = null;
        GetComponent<PlayerMovement>().enabled = true;
        GetComponentInChildren<HeadBob>().enabled = true;
        FindObjectOfType<MouseLook>().enabled = true;
        FindObjectOfType<PauseMenu>().enabled = true;
        OnReading = false;
        NotesBackButton.gameObject.SetActive(false);
    }

    public void Check()
    {

        #region Pickup And Drop
        if (InteractingWithItem && !IsEquipped)
        {
            Pick();
        }
        else if (InteractingWithItem && IsEquipped)
        {
            Drop();
            Pick();
        }

        #endregion
    }
}
