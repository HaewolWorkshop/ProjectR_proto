using System;

[AttributeUsage(AttributeTargets.Class)]
public class FSMStateAttribute : System.Attribute
{
    public readonly int key;

    public FSMStateAttribute(int key)
    {
        this.key = key;
    }
}


public abstract class FSMState<T> where T : IFSMEntity
{
    protected readonly T ownerEntity;

    public abstract void InitializeState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ClearState();

    public FSMState(IFSMEntity owner)
    {
        ownerEntity = (T) owner;
    }
}