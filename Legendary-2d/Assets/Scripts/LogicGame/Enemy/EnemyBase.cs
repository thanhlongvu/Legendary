using UnityEngine;
using DG.Tweening;

public enum MoveAroundDirect
{
    LEFT,
    RIGHT
}

public abstract class EnemyBase : CreationBase
{
    [SerializeField]
    protected bool canSwitchLand;

    [SerializeField]
    protected float timecooldownAttack;

    protected bool isAttack;

    public EnemyBase(int hp, int mp) : base(hp, mp)
    {
        canSwitchLand = false;
        isAttack = false;
    }

    public virtual void MoveAround(MoveAroundDirect dir)
    {
        canSwitchLand = false;
        var xTarget = transform.position.x;
        if (dir == MoveAroundDirect.LEFT)
            xTarget -= GameManager.Instance.DistanceCol;
        else if (dir == MoveAroundDirect.RIGHT)
            xTarget += GameManager.Instance.DistanceCol;

        transform.DOMoveX(xTarget, 0.5f);
    }

    protected abstract bool CanAttack();
}
