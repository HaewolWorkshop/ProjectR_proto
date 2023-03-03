using UnityEngine;

[FSMState((int)Zombie.ZombieStates.Dead)]
public class ZombieDeadState : FSMState<Zombie>
{

    public ZombieDeadState(IFSMEntity owner) : base(owner)
    {
    }
    
    public override void InitializeState()
    {
        ownerEntity.animator.SetTrigger(Zombie.DeathTrigger);
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