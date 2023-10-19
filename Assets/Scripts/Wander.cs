using UnityEngine;
using UnityEngine.AI;
public class Wander : MonoBehaviour
{
    [Header("Wandering Settings")]
    public float Wander_Distance;
    public float Timer;
    public string Anim_Wander;
    public AudioClip Screach;


    NavMeshAgent Agent;
    Animator Anim;
    AudioSource AS;
    float Wait;

    bool Calculate;
    private void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        Calculate = true;
        Anim = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        Anim.SetFloat(Anim_Wander, Agent.velocity.magnitude);
        if(Calculate)
        {
            NavMeshHit Hit;
            Vector3 RandomPos = Random.insideUnitSphere * Wander_Distance;
            NavMesh.SamplePosition(transform.position + RandomPos, out Hit, Wander_Distance, NavMesh.AllAreas);
            Agent.SetDestination(Hit.position);
            Calculate = false;
            Wait = 5f;
        }
        if(Agent.remainingDistance <= Agent.stoppingDistance && !Agent.pathPending)
        {
            if (Wait > 0)
            {
                AS.clip = Screach;
                AS.PlayOneShot(Screach);
                Wait -= Time.deltaTime * Timer;
            }
            else Calculate = true;
        }
        
    }
}
