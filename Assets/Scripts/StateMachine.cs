using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //comma separated list of identifiers
    public enum State
    {
        Attack,
        Defence,
        RunAway,
        BerryPicking
    }
    public State currentState;
    public AIMovement aiMovement;
    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        NextState();
    }
    private void NextState()
    {
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
        }
    }
    private IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        while (currentState == State.Attack)
        {
            aiMovement.AIMoveTowards(aiMovement.player);
            if (!aiMovement.NoticePlayer())
            {
                currentState = State.BerryPicking;
            }
            yield return null;

        }
        Debug.Log("Attack: Exit");
        NextState();
    }
    private IEnumerator DefenceState()
    {
        Debug.Log("Defence: Enter");
        aiMovement.wayPoints = new List<GameObject>();
        aiMovement.findClosestWayPoint();
        //runs every frame
        //sounds like the update
        while (currentState == State.Defence)
        {
            yield return new WaitForSecondsRealtime(10);
            aiMovement.NewWayPoint();
            aiMovement.NewWayPoint();
            aiMovement.NewWayPoint();
            currentState = State.BerryPicking;
            yield return null;

        }
        Debug.Log("Defence: Exit");
        NextState();
    }
    private IEnumerator RunAwayState()
    {
        Debug.Log("RunAway: Enter");
        while (currentState == State.RunAway)
        {
            Debug.Log("Currently Running Away");
            yield return null;
        }
        Debug.Log("RunAway: Exit");
        NextState();
    }
    private IEnumerator BerryPickingState()
    {
        Debug.Log("BerryPicking: Enter");
        aiMovement.findClosestWayPoint();
        while (currentState == State.BerryPicking)
        {
            if (aiMovement.wayPoints.Count<1)
            {
                currentState = State.Defence;
            }
            else
            {
                aiMovement.WayPointsMovement();
                if (aiMovement.NoticePlayer())
                {
                    currentState = State.Attack;
                }

                yield return null;
            }
           

        }
        Debug.Log("BerryPicking: Exit");
        NextState();
    }
}