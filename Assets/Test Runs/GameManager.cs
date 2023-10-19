using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.Playables;
//using UnityEngine.Rendering.PostProcessing;


public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public GameManager Instance;

    [Header("Player Stats")]
    [SerializeField] float Player_Energy = 100f;


    [Header("CutScene Settings")]
    [SerializeField] PlayableAsset Intro;
    PlayableDirector PD;




    [Header("Settings")]
    public GameObject SettingsMenu;
    //[SerializeField] Slider SensSlider;


    [Header("Audio Settings")]
    public AudioMixer Mixer;

    [Header("Objective Settings")]
    [SerializeField] GameObject ObjUpdater;
    GameObject Settings;
    Camera Cam;
    //bool IsActiveted;
    private void Awake()
    {

        Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        if (ObjUpdater != null)
        {
            ObjUpdater.SetActive(false);
        }

        PD = FindObjectOfType<PlayableDirector>();


    }

    private void Start()
    {

        SettingsMenu.SetActive(false);

        Cam = Camera.main;



        if (PD != null)
        {
            PD.playableAsset = Intro;
            PD.Play();
        }

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Graphics(int Index)
    {
        QualitySettings.SetQualityLevel(Index);
    }

    public void Volume(float Value)
    {
        Mixer.SetFloat("Volume", Value);
    }

    public void MotionBlur(bool On)
    {
        //Cam.GetComponent<PostProcessProfile>().RemoveSettings<MotionBlur>();
    }

    public void MotionBlur()
    {

    }
    private void Update()
    {
        Player_Energy -= Time.deltaTime;
    }
}
