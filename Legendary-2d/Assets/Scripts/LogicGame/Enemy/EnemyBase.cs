using UnityEngine;
using DG.Tweening;
using System.Collections;

public enum MoveAroundDirect
{
    LEFT,
    RIGHT
}

public enum EnemyState
{
    MOVE,
    MOVE_AROUND,
    ATTACK
}

public abstract class EnemyBase : CreationBase
{
    [SerializeField]
    protected bool canSwitchLand;
    [SerializeField]
    protected float timecooldownAttack;
    protected EnemyState state;
    protected bool canMove;
    [SerializeField]
    [Range(0, 2)]
    protected float xOffset;
    [SerializeField]
    private Transform model;
    protected abstract bool CanAttack();

    public EnemyBase(int hp, int mp) : base(hp, mp)
    {
        canSwitchLand = false;
        SwitchState(EnemyState.MOVE);
    }


    public override void TakeDamage(int damage = 0)
    {
        if (!IsDead)
            Health -= damage;

        if(IsDead)
            Die();
    }

    public virtual void MoveAround(MoveAroundDirect dir)
    {
        SwitchState(EnemyState.MOVE_AROUND);
        canSwitchLand = false;
        var xTarget = transform.position.x;
        if (dir == MoveAroundDirect.LEFT)
            xTarget -= GameManager.Instance.DISTANCE_COL;
        else if (dir == MoveAroundDirect.RIGHT)
            xTarget += GameManager.Instance.DISTANCE_COL;

        transform.DOMoveX(xTarget, GameManager.Instance.TIME_STEP_X_AXIS);
        StartCoroutine(StopMove());
    }

    protected override void Move(int dir = 0)
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);
    }

    public void SwitchState(EnemyState _state)
    {
        state = _state;
    }

    private IEnumerator StopMove()
    {
        canMove = false;
        yield return StaticObjects.WAIT_TIME_HAFT;
        canMove = true;
    }

    protected void ChangeXByOffset()
    {
        float randX = Random.Range(-xOffset, xOffset);
        model.localPosition += Vector3.right * randX;
    }
}
