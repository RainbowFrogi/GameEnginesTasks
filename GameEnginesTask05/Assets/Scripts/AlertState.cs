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
        enemy.navMeshAgent.isStopped = true; // Enemy kuulee pelaajan, joten se pysähtyy paikoilleen
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0); // Enemy alkaa pyörimään

        searchTimer += Time.deltaTime; // searchTimer on sekuntilaskuri, joka alkaa nollasta

        if(searchTimer > enemy.searchDuration)
        {
            // Tämä if ajetaan jos etsintäaika on yli 4sec. -> Enemy väsyy etsintään -> PatrolStateen
            ToPatrolState();


        }
    }

    void Look()
    {
        // Visualisoidaan säde
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.yellow);
        // Näkösäde on Raycast. Säde lähtee silmästä eteenpäin ja jos säde osuu pelaajaan, vihollinen vihastuu ja menee
        // chasestatee, eli alkaa jahtaamaan. 
        RaycastHit hit; // Informaatio mihin säde osuu tallentuu hit muuttujaan.
        if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {

            Debug.Log("Pelaaja nähty. Lähdetään kimppuun");
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }


}
