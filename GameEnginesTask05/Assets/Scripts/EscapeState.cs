using UnityEngine;
using UnityEngine.AI;

public class EscapeState : IEnemyState
{
    private StatePatternEnemy enemy;
    private Vector3 escapePosition;
    private bool destinationSet = false;

    public EscapeState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void UpdateState()
    {
        Escape();
        Debug.Log("Arrived In Escape State");
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        
    }

    public void ToChaseState()
    {
        
    }

    public void ToEscapeState()
    {
        
    }

    public void ToPatrolState()
    {
        destinationSet = false; // Reset for next time
        enemy.currentState = enemy.patrolState;
    }

    public void ToTrackState()
    {
        
    }

    void Escape()
    {

        // Set escape color indicator
        enemy.indicator.material.color = Color.magenta; // Pink/magenta

        // Set escape position if not already set
        if (!destinationSet)
        {
            SetEscapePosition();
            destinationSet = true;
        }

        // Navigate to escape position
        enemy.navMeshAgent.destination = escapePosition;
        enemy.navMeshAgent.isStopped = false;

        // Increase speed while escaping
        enemy.navMeshAgent.speed = 5f; // Adjust value as needed

        // Check if reached escape position
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            // Reset speed to normal
            enemy.navMeshAgent.speed = 3.5f; // Adjust to your default speed

            // Return to patrol
            ToPatrolState();
        }
    }

    public void SetEscapePosition()
    {
        if (enemy.chaseTarget != null)
        {
            // Calculate direction away from player
            Vector3 directionAwayFromPlayer = (enemy.transform.position - enemy.chaseTarget.position).normalized;

            // Find point at a distance away from current position in that direction
            Vector3 targetPosition = enemy.transform.position + directionAwayFromPlayer * 10f;

            Debug.DrawLine(enemy.transform.position, escapePosition, Color.magenta);
        }
    }
}
