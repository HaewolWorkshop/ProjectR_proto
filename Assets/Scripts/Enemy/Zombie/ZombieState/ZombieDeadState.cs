using UnityEngine;

[FSMState((int)Zombie.ZombieStates.Dead)]
public class ZombieDeadState : FSMState<Zombie>
{

    public ZombieDeadState(IFSMEntity owner) : base(owner)
    {
    }
    
    public override void InitializeState()
    {
        ownerEntity.canDamage = false;
        ownerEntity.StopMove();
        ownerEntity.animator.SetTrigger(Zombie.DeathTrigger);
        ownerEntity.OnDead();
        ownerEntity.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
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