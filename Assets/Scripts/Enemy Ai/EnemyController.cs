using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
public class EnemyController : MonoBehaviour
{
    //public Transform[] ScoutPos;
    //[Range(0, 1f)]
    //public float DistanceToGround;

    [Header("Respawn Settings")]
    [SerializeField] Transform Default_Position;
    [SerializeField] Transform Pos;
    PlayableDirector PD;
    [SerializeField] PlayableAsset PA;
    [SerializeField] Vector3 Pos_Respawn;



    [Header("Enemy Settings")]
    public float SearchingRadius = 50f;
    public Transform Eyes;
    public GameObject DeathCam;
    public Transform CamPos;
    public float Smooth = 10f;
    public LayerMask GroundLayer;

    [Header("Enemy Audio Settings")]
    public AudioClip IdleSound;
    [SerializeField] AudioClip Left_Walk;
    [SerializeField] AudioClip Right_Walk;


    Camera Cam;

    bool Alive;

    GameObject Player;

    NavMeshAgent Agent;

    Animator anim;

    AudioSource AS;


    float Wait;

    bool HighAleart = false;
    float Aleartness = 100f;

    string State;

    string Idle = "Idle";
    string Walk = "Walk";
    string Search = "Search";
    string Chase = "Chase";
    string Hunt = "Hunt";
    string Kill = "Kill";
    int Run;


  
    // Start is called before the first frame update
    void Start()
    {
        DeathCam.SetActive(false);
        PD = FindObjectOfType<PlayableDirector>();
        Agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        Alive = true;
        Cam = Camera.main;
        Run = Animator.StringToHash("Run");
        AS = GetComponent<AudioSource>();
        State = Idle;
        Player=GameObject.Find("Player");
    }


// Update is called once per frame
    void Update()
    {
      
        if(Alive)
        {
            anim.SetFloat("Velocity", Agent.velocity.magnitude);
        }

        if (State==Idle) 
        {
            Vector3 RandomPos = Random.insideUnitSphere * Aleartness;
            NavMeshHit NavHit;
            NavMesh.SamplePosition(transform.position + RandomPos, out NavHit, SearchingRadius, NavMesh.AllAreas);
            //int Randomplace = Random.Range(0, ScoutPos.Length);

            if (HighAleart)
            {
                NavMesh.SamplePosition(Player.transform.position + RandomPos, out NavHit, SearchingRadius, NavMesh.AllAreas);
                Aleartness += 5f;
                if (Aleartness > 20f) 
                {
                    HighAleart = false;
                    Aleartness = 100f;
                }
            }
            anim.SetBool(Run, false);
            Agent.SetDestination(NavHit.position);
            State = Walk;
        }
        if(State==Walk)
        {
            if(Agent.remainingDistance<=Agent.stoppingDistance && !Agent.pathPending)
            {
                State = Search;
                Wait = 5f;
            }
        }
        if (State == Search) 
        {
            if (Wait > 0)
            {
                Wait -= Time.deltaTime;
         
            }
            else State = Idle;
        }
        if(State==Chase)
        {
            anim.SetBool(Run, true);
            Agent.SetDestination(Player.transform.position);
            float Distance = Vector3.Distance(transform.position, Player.transform.position);
            if (Distance >= 10f) 
            {
                State = Hunt;
            }
            else if(Agent.remainingDistance <= Agent.stoppingDistance + 1 && !Agent.pathPending)
            {
                
               
                Player.GetComponent<IsAlive>().Isalive = false;
                Player.GetComponent<PlayerMovement>().enabled = false;
                Cam.GetComponent<MouseLook>().enabled = false;
                DeathCam.SetActive(true);
                DeathCam.transform.position = Cam.transform.position;
                DeathCam.transform.rotation = Cam.transform.rotation;
                Cam.enabled = false;
                State = Kill;
                Invoke("ReSpawn", 2f);
            }
        }
        if(State==Kill)
        {
            anim.SetBool(Run, false);
            DeathCam.transform.position = Vector3.Slerp(DeathCam.transform.position, CamPos.position,Smooth * Time.deltaTime);
            DeathCam.transform.rotation = Quaternion.Slerp(DeathCam.transform.rotation, CamPos.rotation, Smooth * Time.deltaTime);
            Agent.SetDestination(DeathCam.transform.position);
            
            State = Idle;
        }
        if(State==Hunt)
        {
            if (Agent.remainingDistance <= Agent.stoppingDistance && !Agent.pathPending)
            {
               
                State = Search;
                Wait = 5f;
                HighAleart = true;
                Aleartness = 5f;
                Check();
            }
        }

        
    }

    public void Check()
    {
        RaycastHit Hit;
        if(Physics.Linecast(Eyes.position,Player.transform.position, out Hit))
        {
            if(Hit.collider.CompareTag("Player"))
            {
                if(State!=Kill)
                {
                    print("Gotxcha");
                    State = Chase;
                }
            }
        }
    }

    private void ReSpawn()
    {
        
        Cam.enabled = true;
        DeathCam.SetActive(false);
        Player.transform.position = Pos.position;
        Player.transform.rotation = Pos.rotation;
        if(PA!=null)
        {
            PD.playableAsset = PA;
            PD.Play();
        }
        
    }
 
   /* public void IdlePlayer()
    {
        if (IdleSound != null)
        {
            //AS.clip = IdleSound;
        }
        AS.Play();
    }*/

    public void LeftWalk()
    {
        AS.clip = Left_Walk;
        AS.PlayOneShot(Left_Walk);
    }
    public void RightWalk()
    {
        AS.clip = Right_Walk;
        AS.PlayOneShot(Right_Walk);
    }

    /*private void OnAnimatorIK(int layerIndex)
    {
        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IKLeftFootWeight"));
        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, anim.GetFloat("IKLeftFootWeight"));
        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, anim.GetFloat("IKRightFootWeight"));
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, anim.GetFloat("IKRightFootWeight"));

        // Left Foot
        RaycastHit hit;
        // We cast our ray from above the foot in case the current terrain/floor is above the foot position.
        Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, GroundLayer))
        {

            // We're only concerned with objects that are tagged as "Walkable"
            if (hit.transform.tag == "Walkable")
            {

                Vector3 footPosition = hit.point; // The target foot position is where the raycast hit a walkable object...
                footPosition.y += DistanceToGround; // ... taking account the distance to the ground we added above.
                anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));

            }
        }

        ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out hit, DistanceToGround + 1f, GroundLayer))
        {

            if (hit.transform.tag == "Walkable")
            {

                Vector3 footPosition = hit.point;
                footPosition.y += DistanceToGround;
                anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));

            }

        }
    }*/
}
