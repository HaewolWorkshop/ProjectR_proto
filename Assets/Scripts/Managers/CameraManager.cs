using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CameraType
{
    Normal3rdPerson,
    Henshin3rdPerson,
    NormalAim
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private CinemachineVirtualCameraBase[] cameras;

    public void SetCamera(CameraType type)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == (int)type);
        }
    }
}
