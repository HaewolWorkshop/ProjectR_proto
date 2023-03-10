using UnityEngine;

[FSMState((int)Zombie.ZombieStates.Attack)]
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
            if (type != "Attack")
            {
                return;
            }
            ownerEntity.ChangeState(Zombie.ZombieStates.Chase);
        };
    }   
    
    public override void InitializeState()
    {
        ownerEntity.StopMove();
        ownerEntity.animator.SetTrigger(Zombie.AttackTrigger);
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