using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class Objective_System : MonoBehaviour
{
    [Header("Objective Settings")]
    [TextArea]
    [SerializeField] string Objective;
    [SerializeField] int ObjectiveEmitter;
    [SerializeField] TMP_Text Objective_Text;
    [SerializeField] GameObject Next_Objective;
    [SerializeField] bool Want_To_Destroy;

    [Header("Updater")]
    [SerializeField] GameObject ObjUpdater;

    Animator Anim;

    public UnityEvent Event;

    int Update;
    private void Awake()
    {
        Update = Animator.StringToHash("Update");
        Anim = ObjUpdater.GetComponent<Animator>();
        if (Next_Objective != null)
        {
            Next_Objective.SetActive(false);
        }
       
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Event.Invoke();
            ObjUpdater.SetActive(true);
            Objective_Text.text = Objective;
            Anim.SetBool(Update, true);
            if(Next_Objective != null)
            {
                Next_Objective.SetActive(true);
            }
            if(Want_To_Destroy)
            {
                Destroy(gameObject);
            }
        }
    }

}
