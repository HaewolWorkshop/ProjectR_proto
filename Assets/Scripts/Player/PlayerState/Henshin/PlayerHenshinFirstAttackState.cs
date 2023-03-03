using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FSMState((int)Player.States.HenshinFirstAttack)]
public class PlayerHenshinFirstAttackState : FSMState<Player>
{
    private const int AttackAnimKey = 1;
    private const float AnimEndTime = 0.9f;

    public PlayerHenshinFirstAttackState(IFSMEntity owner) : base(owner)
    {
    }

    public override void InitializeState()
    {
        ownerEntity.animator.SetInteger("isAttackMotion", AttackAnimKey);
    }

    public override void UpdateState()
    {
        Debug.Log(ownerEntity.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

        if (ownerEntity.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > AnimEndTime)
        {
            ownerEntity.RevertToPreviousState();
        }
    }

    public override void FixedUpdateState()
    {
    }
    public override void ClearState()
    {
        ownerEntity.animator.SetInteger("isAttackMotion", 0);
    }

}
