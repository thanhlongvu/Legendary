using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MeleeEnemy : EnemyBase
{
    #region VARIABLES
    [SerializeField]
    private PoolName poolName;
    [SerializeField]
    private LayerMask layerBarrier;
    [SerializeField]
    private float distanceToSwitchLand;
    private float timeAttack;
    [SerializeField]
    private bool IsChangeXOffset = true;

    private int healthDefault;
    private int manaDefault;

    [SerializeField]
    private Sound soundDie;
    #endregion

    #region UNITY_FUNCTIONS
    private void Awake()
    {
        //Set default
        healthDefault = Health;
        manaDefault = Mana;
    }
    void Start()
    {
        timeAttack = 0;
        canMove = true;
        if(IsChangeXOffset)
            ChangeXByOffset();
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(GameTag.PLAYER_WEAPON))
        {
            //TakeDamage(20);
        }

        if (other.gameObject.tag.Equals(GameTag.FORTRESS))
        {
            //TODO: basic example
            PoolManager.Instance.PushPool(gameObject, poolName.ToString());
        }
    }

    private void OnEnable()
    {
        Health = healthDefault;
        Mana = manaDefault;
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

    public override void Die()
    {
        SoundManager.Instance.PlaySound(soundDie);
        PoolManager.Instance.PushPool(gameObject, poolName.ToString());
    }
    #endregion
}