using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public partial class Player : FSMPlayer<Player>, IFSMEntity
{
    public enum States : int
    {
        Global,
        
        NormalMove,
        NormalSprint,
        NormalStealth,
        NormalJump,
        NormalThrow,
        NormalFall,
        
        Disengage,
        Henshin,
        
        HenshinMove,
        HenshinGrab,
        HenshinJump,
        HenshinFalling,
        HenshinGuard,
        HenshinAttackIdle,
        HenshinFirstAttack
    }

    public bool wtf = false;

    private const float groundDist = 0.3f;

    public Material henshinMat;// 임시

    public Collider[] groundObjects { get; private set; }
    public bool isGrounded => (groundObjects?.Length ?? 0) != 0;

    public Rigidbody rigidbody { get; private set; }
    public Animator animator { get; private set; }

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask grabLayer;
    public LayerMask GrabLayer => grabLayer;

    [SerializeField] private Animator normalModel;
    public Animator NormalModel => normalModel;
    
    [SerializeField] private Animator henshinModel;
    public Animator HenshinModel => henshinModel;


    [SerializeField] private Transform grabPoint;
    public Transform GrabPoint => grabPoint;



    [SerializeField] private GameObject stepNormalRayUpper;
    [SerializeField] private GameObject stepNormalRayLower;
    [SerializeField] private float stepHeight = .0f;
    [SerializeField] private float stepSmooth = .0f;

    [SerializeField] private HitBox hitBox = null;
    // 임시
    [SerializeField] private PlayerData[] data;
    public PlayerData[] Data => data;

    protected void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        stepNormalRayUpper.transform.position = new Vector3(stepNormalRayUpper.transform.position.x, stepHeight, stepNormalRayUpper.transform.position.z);
        hitBox.SetPlayer(this);
        
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
        wtf = false;
    }


    private void CheckGround()
    {
        groundObjects = Physics.OverlapSphere(transform.position, groundDist, groundLayer);
    }

    public GameObject GetUpperRay()
    {
        return stepNormalRayUpper;
    }

    public GameObject GetLowerRay()
    {
        return stepNormalRayLower;
    }

    public float GetStepHeight()
    {
        return stepHeight;
    }

    public float GetStepSmooth()
    {
        return stepSmooth;
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void SetHitBox(bool isOn)
    {
        hitBox.gameObject.SetActive(isOn);
    }
}