using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : FSMPlayer<Player>, IFSMEntity
{
    public enum PlayerStates : int
    {
        Move = 0,
        Dash,
        Guard,
        Jump,
        Fall,
        Attack,
        Skill,
        Stealth,
        Max
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
        
        SetUp(PlayerStates.Move);
    }

    protected override void Update()
    {
        UpdateInputs();

        base.Update();
    }
}