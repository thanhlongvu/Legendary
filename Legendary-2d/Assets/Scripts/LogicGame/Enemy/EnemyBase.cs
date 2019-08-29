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

    protected abstract bool CanAttack();

    public EnemyBase(int hp, int mp) : base(hp, mp)
    {
        canSwitchLand = false;
        SwitchState(EnemyState.MOVE);
    }

    public virtual void MoveAround(MoveAroundDirect dir)
    {
        SwitchState(EnemyState.MOVE_AROUND);
        canSwitchLand = false;
        var xTarget = transform.position.x;
        if (dir == MoveAroundDirect.LEFT)
            xTarget -= GameManager.Instance.DistanceCol;
        else if (dir == MoveAroundDirect.RIGHT)
            xTarget += GameManager.Instance.DistanceCol;

        transform.DOMoveX(xTarget, 0.5f);
        StartCoroutine(StopMove());
    }

    public override void Move()
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
}
