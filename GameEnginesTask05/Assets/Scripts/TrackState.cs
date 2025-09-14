using UnityEngine;

public class TrackState : IEnemyState
{
    private StatePatternEnemy enemy;
    private Vector3 lastKnownPosition;

    public TrackState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        Track();
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
        enemy.currentState = enemy.chaseState;
    }

    public void ToPatrolState()
    {

    }
    public void ToTrackState()
    {

    }

    public void ToEscapeState()
    {
        enemy.currentState = enemy.escapeState;
    }

    void Track()
    {
        // Blue indicator for tracking state
        enemy.indicator.material.color = Color.blue;
        
        // Move to the last known position of the player
        enemy.navMeshAgent.destination = lastKnownPosition;
        enemy.navMeshAgent.isStopped = false;
        
        // Check if we've reached the last known position
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            // We've reached the last known position, go to alert state to look around
            ToAlertState();
        }
    }

    void Look()
    {
        // Visualize the sight ray
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.blue);
        // Visualise the last known position
        Debug.DrawLine(enemy.transform.position, lastKnownPosition, Color.cyan);

        // Check if we can see the player again
        RaycastHit hit;
        if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Player spotted again during tracking!");
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    // Method to set the last known position when transitioning to this state
    public void SetLastKnownPosition(Vector3 position)
    {
        lastKnownPosition = position;
    }
}
