using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeEnemy : EnemyBase
{
#region VARIABLES
    private Transform player;
    [SerializeField]
    private LayerMask layerBarrier;
    [SerializeField]
    private float distanceToSwitchLand;

    private float timeAttack;
#endregion

#region UNITY_FUNCTIONS
    void Start()
    {
        timeAttack = 0;
        canMove = true;
    }

    void Update()
    {
        //Debug
        GizmoManager.DrawLine(transform.position, transform.position + Vector3.down * distanceToSwitchLand);

        //Move action
        if ((state == EnemyState.MOVE) && canMove)
            Move();

        //Scan action
        if (ScanBarrier())
        {
            if (canSwitchLand)
            {
                //TODO: left or right
                var randDirect = Random.Range(0, 100) / 50;

                MoveAround((MoveAroundDirect)randDirect);
            }
            else
            {
                //Attack
                BaseAttack();
            }
        }
        else
        {
            if (state != EnemyState.MOVE && canMove)
                state = EnemyState.MOVE;
        }

        //check time attack cooldown
        if (timeAttack > 0)
            timeAttack -= Time.deltaTime;

    }
    #endregion


    #region OOP
    public MeleeEnemy(int hp, int mp) : base(hp, mp)
    {
    }

    protected override bool CanAttack()
    {
        return ScanBarrier();
    }

    public override void BaseAttack()
    {
        if(timeAttack <= 0)
        {
            state = EnemyState.ATTACK;
            timeAttack += timecooldownAttack;
            LogSystem.LogError("Attack!!!!!!!");
            return;
        }
    }

    private bool ScanBarrier()
    {
        RaycastHit2D[] hit = new RaycastHit2D[3];
        if (Physics2D.RaycastNonAlloc(transform.position, Vector2.down, hit, distanceToSwitchLand, layerBarrier) > 0)
        {
            return true;
        }

        return false;
    }
#endregion
}