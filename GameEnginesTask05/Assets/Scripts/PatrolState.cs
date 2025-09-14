using UnityEngine;

public class PatrolState : IEnemyState
{
    private StatePatternEnemy enemy;
    int nextWaypoint; 

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        // Updaten ajo halutaan siirt‰‰ t‰h‰n metodiin. 
        Patrol();
        Look();
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        // T‰t‰ ei k‰ytet‰ kun ollan jo PatrolStatessa. 
    }

    public void ToTrackState()
    {

    }

    public void ToEscapeState()
    {

    }

    void Patrol()
    {
        enemy.indicator.material.color = Color.green;

        enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position;

        enemy.navMeshAgent.isStopped = false;

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            // T‰m‰ if ajetaan vain jos enemy on sallitun et‰isyyden p‰‰ss‰ waypointista JA polun laskenta on p‰‰ttynyt. 
            nextWaypoint = (nextWaypoint + 1) % enemy.waypoints.Length; // Looppaa waypointit l‰pi. 

            // Randomi
            //nextWaypoint = Random.Range(0, enemy.waypoints.Length);

        }
    }

    void Look()
    {
        // Visualisoidaan s‰de
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.green);
        // N‰kˆs‰de on Raycast. S‰de l‰htee silm‰st‰ eteenp‰in ja jos s‰de osuu pelaajaan, vihollinen vihastuu ja menee
        // chasestatee, eli alkaa jahtaamaan. 
        RaycastHit hit; // Informaatio mihin s‰de osuu tallentuu hit muuttujaan.
        if(Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {

            Debug.Log("Pelaaja n‰hty. L‰hdet‰‰n kimppuun");
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }
}
