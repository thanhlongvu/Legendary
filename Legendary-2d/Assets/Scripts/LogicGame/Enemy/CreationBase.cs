using UnityEngine;

public abstract class CreationBase : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int mana;
    [SerializeField]
    private float speed;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public int Mana
    {
        get
        {
            return mana;
        }
        set
        {
            mana = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public CreationBase(int hp, int mp)
    {
        health = hp;
        mana = mp;
    }

    protected abstract void Move(int dir = 0);

    public abstract void BaseAttack();

    public abstract void TakeDamage(int damage = 0);

    public abstract void Die();

    public virtual void SkillAttack() { }

    public bool IsDead
    {
        get
        {
            return Health <= 0;
        }
        private set { }
    }
}
