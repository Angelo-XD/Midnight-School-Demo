using UnityEngine;

public class FlashLight : MonoBehaviour
{

    public GameObject Light;

    bool IsOn;
    // Start is called before the first frame update
    void Start()
    {
        Light.SetActive(false);
        IsOn = false;
    }

    // Update is called once per frame
    void Update()
    {


       /* if (Input.GetKeyDown(KeyCode.F))
        {
            IsOn = !IsOn;
            if (IsOn)
            {
                Light.SetActive(true);
            }
            if (!IsOn)
            {
                Light.SetActive(false);
            }
        }
       */
    }

    public void FlahslightInteract()
    {
        IsOn = !IsOn;
        if (IsOn)
        {
            Light.SetActive(true);
        }
        if (!IsOn)
        {
            Light.SetActive(false);
        }
    }
}