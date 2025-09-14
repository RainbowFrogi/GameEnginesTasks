using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour
{
    public float searchDuration;
    public float searchTurnSpeed;
    public float sightRange;
    public Transform[] waypoints; // Taulukko waypointeista
    public Transform eye;
    public MeshRenderer indicator; // Laatikko enemyn p��ll�, joka vaihtaa v�ri�.

    public Transform chaseTarget;

    [HideInInspector]
    public IEnemyState currentState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public TrackState trackState;
    [HideInInspector]
    public EscapeState escapeState;

    public NavMeshAgent navMeshAgent;


    private void Awake()
    {
        // Luodaan uudet objektit luokista
        patrolState = new PatrolState(this);
        // Tee samat asiat muihin tiloihin mit� teit patrolStateen, objekti, konstruktori ja interfacen implementointi. 
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);
        trackState = new TrackState(this);
        escapeState = new EscapeState(this);



        navMeshAgent = GetComponent<NavMeshAgent>();


    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = patrolState; // Kerrotaan tilakoneelle, ett� startissa k�yt�ss� oleva tila on patrolState.
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

}
