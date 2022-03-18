using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIManager : BaseManager
{
   

    public enum State
    {
        HighHP,
        LowHP,
        Dead
    }
    private PlayerManager _playerManager;
    public State currentState;
    [SerializeField] protected CanvasGroup _buttonGroup;
    [SerializeField] protected Animator _anim;


    public override void TakeTurn()
    {
        switch (currentState)
        {

            case State.HighHP:
                HighHpState();
                break;
            case State.LowHP:
                LowHPState();

                break;
            case State.Dead:
                DeadState();

                break;
            default:
                break;
        }
        _buttonGroup.interactable = true;

    }
    protected override void EndTurn()
    {
        StartCoroutine(Wait());

        _playerManager.TakeTurn();


    }
    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(2f);
    }

    void HighHpState()
    {
        if (_health < 40 && _health != 0)
        {
            currentState = State.LowHP;
        }
        if (_health == 0)
        {
            currentState = State.Dead;
        }

        int randomAttack = Random.Range(0, 10);
        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 2:
                Splash();
                break;
            case int i when i > 2 && i <= 8:
                IronTail();
                break;
            case int i when i > 8 && i <= 9:
                SelfDestruct();
                break;
            default:
                break;
        }


    }
    void LowHPState()
    {
        if (_health > 40 && _health != 0)
        {
            currentState = State.HighHP;
        }
        if (_health == 0)
        {
            currentState = State.Dead;
        }
        int randomAttack = Random.Range(0, 10);
        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 2:
                IronTail();
                break;
            case int i when i > 2 && i <= 8:
                Rest();
                break;
            case int i when i > 8 && i <= 9:
                SelfDestruct();
                break;
            default:
                break;
        }


    }
    void DeadState()
    {
        Debug.Log("AI dead you win");
    }
    protected override void Start()
    {
        base.Start();
        _playerManager = GetComponent<PlayerManager>();
    }
    public void Splash()
    {
        Debug.Log("Ai casts Splash");

        _playerManager.DealDamage(10f);
        _anim.SetTrigger("Splash");

        EndTurn();
    }
    public void IronTail() 
    { 
        Debug.Log("Ai casts Iron Tail");

        _playerManager.DealDamage(50f);
        EndTurn();
    }
    public void Rest()
    {
        Debug.Log("Ai casts Rest");

        Heal(30f);
        EndTurn();
    }
    public void SelfDestruct()
    {
        Debug.Log("Ai casts SelfDestruct");

        DealDamage(_maxHealth);
        _playerManager.DealDamage(80f);
        EndTurn();
    }
}
