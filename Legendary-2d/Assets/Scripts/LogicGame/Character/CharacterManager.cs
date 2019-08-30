using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public enum CharacterState
{
    MOVE,
    ATTACK,
    IDLE
}

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterManager : CreationBase
{
    
#region VARIABLES
    private Rigidbody2D rb;
    [HideInInspector]
    public int directMove;
    [SerializeField]
    private float timecooldownMove;
    [HideInInspector]
    public float timeMove;
    public CharacterState state;
    [SerializeField]
    private GameObject baseAttackPrefab;
    [SerializeField]
    private float timeCooldownBaseAttack;
    private float timeAttack;
#endregion



#region UNITY_FUNCTIONS
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        SwitchState(CharacterState.IDLE);
        //Move event
        EventManager.AddListener(GameEvent.MOVE_CHARACTER, MoveByDirect);
        EventManager.AddListener(GameEvent.MOVE_CHARACTER, ResetTimecooldownMove);
        //add sound

        //AttackEvent
        EventManager.AddListener(GameEvent.ATTACK_CHARACTER, BaseAttack);
        //sound

        //Default variable
        timeMove = 0;
        timeAttack = 0;
    }

    private void Update()
    {
        if (timeMove > 0)
            timeMove -= Time.deltaTime;

        if (timeAttack > 0)
            timeAttack -= Time.deltaTime;
    }

    private void MoveByDirect()
    {
        if (CheckValidDirect())
        {
            SwitchState(CharacterState.MOVE);
            Move(directMove);
        }
    }

    public void SwitchState(CharacterState _state)
    {
        state = _state;
    }

    private bool CheckValidDirect()
    {
        if(Mathf.Abs(transform.position.x - GameManager.Instance.MAX_X) < GameManager.Instance.DISTANCE_COL && directMove == 1 ||
            Mathf.Abs(transform.position.x - GameManager.Instance.MIN_X) < GameManager.Instance.DISTANCE_COL && directMove == -1)
        {
            return false;
        }
        return true;
    }
#endregion



#region OOP
    public CharacterManager(int hp, int mp) : base(hp, mp)
    {
    }

    public override void BaseAttack()
    {
        if(timeAttack <= 0)
        {
            timeAttack = timeCooldownBaseAttack;
            PoolManager.Instance.PopPool(PoolName.SPEAR.ToString(), transform.position, Quaternion.identity);
        }
    }

    protected override void Move(int dir = 0)
    {
        DOTweenModulePhysics2D.DOMoveX(rb, dir * GameManager.Instance.DISTANCE_COL + transform.position.x, GameManager.Instance.TIME_STEP_X_AXIS);
    }

    private void ResetTimecooldownMove()
    {
        timeMove = timecooldownMove / Speed;
    }

    public override void TakeDamage(int damage = 0)
    {
        
    }

    public override void Die()
    {
        
    }
    #endregion
}
