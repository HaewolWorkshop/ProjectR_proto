using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[FSMState((int) Player.States.HenshinAttackIdle)]
public class PlayerHenshinAttackState : FSMState<Player>
{
    public PlayerHenshinAttackState(IFSMEntity owner) : base(owner) { }

    private Vector2 moveInput;

    public override void InitializeState()
    {
        ownerEntity.SetAction(Player.ButtonActions.Attack, OnAttack);

        ownerEntity.onMove = (x) => moveInput = x;


        if (!ownerEntity.animator.GetBool("isAttacking"))
        {
            ownerEntity.animator.SetBool("isAttacking", true);
            OnAttackMotion();
        }
    }

    public override void UpdateState()
    {
        if(moveInput != Vector2.zero)
        {
            ownerEntity.animator.SetBool("isAttacking", false);
            ownerEntity.ChangeState(Player.States.HenshinMove);
        }
    }

    public override void FixedUpdateState()
    {

    }

    public override void ClearState()
    {
        ownerEntity.ClearAction(Player.ButtonActions.Attack);
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
