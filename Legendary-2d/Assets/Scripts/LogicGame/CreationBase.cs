
public abstract class CreationBase : Singleton<CreationBase>
{
    public int Health { get; set; }
    public int Mana { get; set; }

    public virtual void InstanceSkill() { }

    public virtual bool IsDead()
    {
        if(Health <= 0)
            return true;

        return false;
    }
}
