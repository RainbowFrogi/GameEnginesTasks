using UnityEngine;

public interface IEnemyState
{

    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();

    void ToTrackState();

    void ToEscapeState();


}
