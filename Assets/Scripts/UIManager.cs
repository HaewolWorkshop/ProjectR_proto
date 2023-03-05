using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Image hpBar;
    [SerializeField] private Image bpBar;

    public void SetHPBar(float progress)
    {
        hpBar.fillAmount = progress;
    }

    public void SetBPBar(float progress)
    {
        bpBar.fillAmount = progress;
    }
}