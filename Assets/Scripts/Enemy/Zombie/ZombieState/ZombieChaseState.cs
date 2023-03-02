using UnityEngine;

public class ZombieChaseState : FSMState<Zombie>
{

    public ZombieChaseState(IFSMEntity owner) : base(owner)
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
        var player = ownerEntity.player;
        if (player)
        {
            var playerPosition = player.transform.position;
            if (Vector3.Distance(playerPosition, ownerEntity.transform.position) <= ownerEntity.attackRange)
            {
                ownerEntity.ChangeState(Zombie.ZombieStates.Attack);
            }
            else
            {
                ownerEntity.SetMoveTarget(player.transform.position);
            }
            
        }
    }

    public override void ClearState()
    {
        
    }
}