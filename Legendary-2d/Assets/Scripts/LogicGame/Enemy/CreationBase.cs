using UnityEngine;

public abstract class CreationBase : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int mana;
    [SerializeField]
    private int speed;
    public int Health
    {
        get
        {
            return health;
        }
    }
    public int Mana
    {
        get
        {
            return mana;
        }
    }

    public int Speed
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

    public abstract void Move();

    public abstract void BaseAttack();

    public virtual void SkillAttack() { }

    public virtual bool IsDead()
    {
        if(Health <= 0)
            return true;

        return false;
    }
}
