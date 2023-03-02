using UnityEngine;

public class ZombieAttackState : FSMState<Zombie>
{

    public ZombieAttackState(IFSMEntity owner) : base(owner)
    {
        ownerEntity.OnAttack += () =>
        {
            // TODO 플레이어 가격
            Debug.Log($"{ownerEntity.name} 플레이어 공격");
        };
        ownerEntity.OnAnimationEnd += (type) =>
        {
            Debug.Log($"{ownerEntity.name} {type} 애니메이션 종료");
            ownerEntity.ChangeState(Zombie.ZombieStates.Chase);
        };
    }   
    
    public override void InitializeState()
    {
        ownerEntity.StopMove();
        ownerEntity.animator.SetTrigger(Zombie.Attack);
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