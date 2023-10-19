using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public AudioSource PlayerAudio;
    public PlayerMovement PM;
    public MouseLook ML;
    GameObject PausePanel;
    Animator PauseMenuAnim;
    GameObject Player;

    public GameObject Inuts;

    public bool IsPaused;
  

    float Wait;

    int IsPause;
    void Start()
    {
        PauseMenuAnim = GetComponent<Animator>();
        PausePanel = GameObject.Find("Pause Panel");
        PausePanel.SetActive(false);
        IsPaused = false;
        IsPause = Animator.StringToHash("IsPaused");
        Player=GameObject.Find("Player");
      
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            IsPaused = !IsPaused;
            if (IsPaused)
            {
                Pause();
            }
            else if (!IsPaused)
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        Inuts.SetActive(false);
        PM.enabled = false;
        ML.enabled = false;
        HeadBob.Instance.enabled = false;
        //Cursor.lockState = CursorLockMode.Confined;
        PlayerAudio.enabled = false;
        PausePanel.SetActive(true);
        IsPaused = true;
        
        //Time.timeScale = 0;
    }
    public void Resume()
    {
        Inuts.SetActive(true);
        ML.enabled = true;
        PM.enabled = true;
        HeadBob.Instance.enabled = true;
        //Cursor.lockState = CursorLockMode.Locked;
        PlayerAudio.enabled = true;
        PausePanel.SetActive(false);
        IsPaused = false;    
      
        //Time.timeScale = 1;
    }
}
