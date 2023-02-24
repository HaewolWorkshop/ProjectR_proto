public abstract class FSMState<T> where T : IFSMEntity
{
    protected T ownerEntity;

    public abstract void InitializeState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ClearState();

    public FSMState(IFSMEntity owner)
    {
        ownerEntity = (T)owner;
    }
}
