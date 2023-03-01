using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[FSMState((int)Player.States.Global)]
public class PlayerGlobalState : FSMState<Player>
{
    public PlayerGlobalState(IFSMEntity owner) : base(owner)
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