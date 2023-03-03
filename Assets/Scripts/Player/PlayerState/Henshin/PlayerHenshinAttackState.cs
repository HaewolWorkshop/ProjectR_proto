using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FSMState((int) Player.States.HenshinAttackIdle)]
public class PlayerHenshinAttackState : FSMState<Player>
{
    public PlayerHenshinAttackState(IFSMEntity owner) : base(owner) { }

    public override void InitializeState()
    {
        ownerEntity.SetAction(Player.ButtonActions.Sprint, OnMove);
        ownerEntity.SetAction(Player.ButtonActions.Attack, OnAttack);


        if (!ownerEntity.animator.GetBool("isAttacking"))
        {
            ownerEntity.animator.SetBool("isAttacking", true);
            OnAttackMotion();
        }
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

    private void OnMove(bool isOn)
    {
        if(isOn)
        {
            ownerEntity.animator.SetBool("isAttacking", false);
            ownerEntity.ChangeState(Player.States.HenshinMove);
        }
    }

    private void OnAttackMotion()
    {
        ownerEntity.ChangeState(Player.States.HenshinFirstAttack);
    }

    private void OnAttack(bool isOn)
    {
        if (isOn)
        {
            ownerEntity.animator.SetBool("isAttacking", false);
            ownerEntity.ChangeState(Player.States.HenshinAttackIdle);
        }
    }


}
