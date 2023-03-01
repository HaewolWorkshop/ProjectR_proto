using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : FSMPlayer<Player>, IFSMEntity
{
    public enum States : int
    {
        Global,
        
        NormalMove,
        NormalDash,
        NormalGuard,
        NormalJump,
        NormalFall,
        NormalAttack,
        NormalStealth,
        
        BackToNormal,
        Henshin,
        
        HenshinMove,
        HenshinJump,
        HenshinGuard,
    }

    public Rigidbody rigidbody { get; private set; }
    public Animator animator { get; private set; }


    [SerializeField] private PlayerData[] data;
    public PlayerData Data => data[0];

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        
        InitInputs();
        
        SetUp(States.NormalMove);
    }

    protected override void Update()
    {
        UpdateInputs();

        base.Update();
    }
}