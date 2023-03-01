using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int) Player.States.Disengage)]
public class PlayerDisengageState : FSMState<Player>
{
    public PlayerDisengageState(IFSMEntity owner) : base(owner)
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
    }

    public override void ClearState()
    {
    }
}