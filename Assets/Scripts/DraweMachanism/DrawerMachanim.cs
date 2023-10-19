using UnityEngine;

public class DrawerMachanim : MonoBehaviour
{
    public LayerMask Drawer;
    RaycastHit Hit;

    float Range = 4f;

    Camera cam;

    bool IsInteractingwithdrawer;
    bool Isopened;

    int Opened;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        Opened = Animator.StringToHash("IsOpen");
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out Hit, Range, Drawer))
        {
            IsInteractingwithdrawer = true;
        }
        else IsInteractingwithdrawer = false;

        if(IsInteractingwithdrawer && Input.GetKeyDown(KeyCode.E))
        {
            Open();
        }
    }

    void Open()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out Hit, Range, Drawer))
        {
            if(Hit.collider.CompareTag("Drawer"))
            {
                Hit.transform.gameObject.GetComponent<Drawer>().Opening();
            }
        }
    }
 
}
