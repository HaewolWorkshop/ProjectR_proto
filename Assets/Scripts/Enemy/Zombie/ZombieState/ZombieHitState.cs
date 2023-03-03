using UnityEngine;

[FSMState((int)Zombie.ZombieStates.Hit)]
public class ZombieHitState : FSMState<Zombie>
{

    public ZombieHitState(IFSMEntity owner) : base(owner)
    {
        ownerEntity.OnAnimationEnd += (type) =>
        {
            if (type != "Hit")
            {
                return;
            }
            ownerEntity.RevertToPreviousState();
        };
    }
    
    public override void InitializeState()
    {
        ownerEntity.canDamage = false;
        ownerEntity.StopMove();
        ownerEntity.animator.SetTrigger(Zombie.HitTrigger);
    }

    public override void UpdateState()
    {
    }

    public override void FixedUpdateState()
    {
    }

    public override void ClearState()
    {
        ownerEntity.canDamage = true;
    }
}