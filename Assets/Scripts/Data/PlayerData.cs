using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Data/Player Data", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    //[Header("카메라")]

    //[SerializeField] private bool inverseY;
    //public bool InverseY => inverseY;

    //[SerializeField] private float lookSpeed;
    //public float LookSpeed => lookSpeed;


    //[SerializeField] private float cameraMaxYAngle;
    //public float CameraMaxYAngle => cameraMaxYAngle;

    //[SerializeField] private float cameraMinYAngle;
    //public float CameraMinYAngle => cameraMinYAngle;

    //[SerializeField] private float cameraCenterAngle;
    //public float CameraCenterAngle => cameraCenterAngle;

    //[SerializeField] private float cameraReCenterSpeed;
    //public float CameraReCenterSpeed => cameraReCenterSpeed;



    [Header("이동")]

    [SerializeField] private float moveSpeed;
    public float MoveSpeed => moveSpeed;

    [SerializeField] private float rotationSpeed;
    public float RotationSpeed => rotationSpeed;


    [SerializeField] private float stealthMoveMultiplier;
    public float StealthMoveMultiplier => stealthMoveMultiplier;


    [SerializeField] private float sprintSpeedMultiplier;
    public float SprintSpeedMultiplier => sprintSpeedMultiplier;

    [SerializeField] private float sprintXAxisMultiplier;
    public float SprintXAxisMultiplier => sprintXAxisMultiplier;



    [SerializeField] private float jumpPower;
    public float JumpPower => jumpPower;

    [SerializeField] private float airMoveSpeed;
    public float AirMoveSpeed => airMoveSpeed;

    [SerializeField] private float fallingSpeed;
    public float FallingSpeed => fallingSpeed;
}