using UnityEngine;

[FSMState((int)Zombie.ZombieStates.Idle)]
public class ZombieIdleState : FSMState<Zombie>
{

    public ZombieIdleState(IFSMEntity owner) : base(owner)
    {
    }
    
    public override void InitializeState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void FixedUpdateState()
    {
    }

    public override void ClearState()
    {
    }
}