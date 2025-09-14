using UnityEngine;

public class AlertState : IEnemyState
{
    private StatePatternEnemy enemy;
    float searchTimer; 

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Search();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        
    }

    public void ToChaseState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.patrolState;
    }

    public void ToTrackState()
    {

    }

    public void ToEscapeState()
    {

    }
    void Search()
    {
        enemy.indicator.material.color = Color.yellow;
        enemy.navMeshAgent.isStopped = true; // Enemy kuulee pelaajan, joten se pys�htyy paikoilleen
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0); // Enemy alkaa py�rim��n

        searchTimer += Time.deltaTime; // searchTimer on sekuntilaskuri, joka alkaa nollasta

        if(searchTimer > enemy.searchDuration)
        {
            // T�m� if ajetaan jos etsint�aika on yli 4sec. -> Enemy v�syy etsint��n -> PatrolStateen
            ToPatrolState();


        }
    }

    void Look()
    {
        // Visualisoidaan s�de
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.yellow);
        // N�k�s�de on Raycast. S�de l�htee silm�st� eteenp�in ja jos s�de osuu pelaajaan, vihollinen vihastuu ja menee
        // chasestatee, eli alkaa jahtaamaan. 
        RaycastHit hit; // Informaatio mihin s�de osuu tallentuu hit muuttujaan.
        if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {

            Debug.Log("Pelaaja n�hty. L�hdet��n kimppuun");
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }


}
