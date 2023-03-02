using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : FSMPlayer<Player>, IFSMEntity
{
    public enum States : int
    {
        Global,
        
        NormalMove,
        NormalSprint,
        NormalStealth,
        NormalJump,
        NormalFall,
        
        Disengage,
        Henshin,
        
        HenshinMove,
        HenshinJump,
        HenshinGuard,
    }

    private const float groundDist = 0.2f;

    public Material henshinMat;// 임시

    public bool isGrounded { get; private set; }

    public Rigidbody rigidbody { get; private set; }
    public Animator animator { get; private set; }

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Animator normalModel;
    public Animator NormalModel => normalModel;
    
    [SerializeField] private Animator henshinModel;
    public Animator HenshinModel => henshinModel;

    // 임시
    [SerializeField] private PlayerData[] data;
    public PlayerData[] Data => data;

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

    protected override void FixedUpdate()
    {
        CheckGround();

        base.FixedUpdate();
    }


    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out var hit, groundDist, groundLayer);
    }
}