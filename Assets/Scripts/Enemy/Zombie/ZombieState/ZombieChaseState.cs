using UnityEngine;

[FSMState((int)Zombie.ZombieStates.Chase)]
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
        // 플레이어가 시야 밖으로 벗어나면 (또는 물체에 가려졌다면)
        //if (!ownerEntity.IsPlayerInSight())
        //{
        //    // Idle로 돌아감
        //    ownerEntity.ChangeState(Zombie.ZombieStates.Idle);
        //    return;
        //}
        
        // 플레이어가 공격 범위 이내로 들어오면 공격으로 전환
        var player = ownerEntity.player;
        var playerPosition = player.transform.position;
        if (Vector3.Distance(playerPosition, ownerEntity.transform.position) <= ownerEntity.attackRange)
        {
            ownerEntity.ChangeState(Zombie.ZombieStates.Attack);
        }
        // 공격 범위 이내가 아니면 계속 쫓아감
        else
        {
            ownerEntity.SetMoveTarget(player.transform.position);
        }
    }

    public override void ClearState()
    {
    }
}