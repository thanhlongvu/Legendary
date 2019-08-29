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
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Move();

        GizmoManager.DrawLine(transform.position, transform.position + Vector3.down * distanceToSwitchLand);
        if (canSwitchLand && Time.frameCount % 10 == 0)
            if (NearBarrier())
            {
                //TODO: left or right
                var randDirect = Random.Range(0, 100) / 50;

                MoveAround((MoveAroundDirect)randDirect);
            }

        //check time attack cooldown
        if (timeAttack > 0)
            timeAttack -= Time.deltaTime;

        //Enemy attack
        BaseAttack();
    }
#endregion

#region OOP
    public MeleeEnemy(int hp, int mp) : base(hp, mp)
    {
    }

    protected override bool CanAttack()
    {
        return NearBarrier();
    }

    public override void BaseAttack()
    {
        if(!canSwitchLand && timeAttack <= 0 && CanAttack())
        {
            isAttack = true;
            timeAttack += timecooldownAttack;
            LogSystem.LogError("Attack!!!!!!!");
            return;
        }

        if(!CanAttack())
        {
            isAttack = false;
        }
    }

    public override void Move()
    {
        if(!isAttack)
            transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    private bool NearBarrier()
    {
        RaycastHit2D hit;
        if (hit = Physics2D.Raycast(transform.position, Vector2.down, distanceToSwitchLand, layerBarrier))
        {
            //LogSystem.LogWarning(hit.transform.name);
            return true;
        }

        return false;
    }
#endregion
}