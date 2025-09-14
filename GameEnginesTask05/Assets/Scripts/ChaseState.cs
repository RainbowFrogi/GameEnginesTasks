using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Chase();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Light"))
        {
            ToEscapeState();
        }
    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void ToPatrolState()
    {

    }
    public void ToTrackState()
    {
        enemy.currentState = enemy.trackState;
    }

    public void ToEscapeState()
    {
        enemy.currentState = enemy.escapeState;
    }

    void Chase()
    {
        enemy.indicator.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false; 


    }

    void Look()
    {

        // B-A eli miinuslasku pelaajasta silm‰‰n.
        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position;

        // Visualisoidaan s‰de
        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.yellow);



        // N‰kˆs‰de on Raycast. S‰de l‰htee silm‰st‰ eteenp‰in ja jos s‰de osuu pelaajaan, vihollinen vihastuu ja menee
        // chasestatee, eli alkaa jahtaamaan. 
        RaycastHit hit; // Informaatio mihin s‰de osuu tallentuu hit muuttujaan.
        if (Physics.Raycast(enemy.eye.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {

            Debug.Log("Pelaaja n‰hty. L‰hdet‰‰n kimppuun");
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
        else
        {
            // Pelaaja katosi n‰kyvist‰. Tallennetaan viimeisin tunnettu sijainti ja menn‰‰n TrackStateen.
            Debug.Log("Pelaaja katosi n‰kyvist‰. Menn‰‰n trackstateen");
            Vector3 lastKnownPosition = enemy.chaseTarget.position;
            enemy.trackState.SetLastKnownPosition(lastKnownPosition);
            ToTrackState();
        }
    }
}
