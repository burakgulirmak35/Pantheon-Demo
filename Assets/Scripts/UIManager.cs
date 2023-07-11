using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PrefabsSO prefabs;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI txt_Power;
    [Space]
    private int PowerAmount;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        LoadResources();
    }

    public void AddPower(int amount)
    {
        PowerAmount += amount;
        txt_Power.text = PowerAmount.ToString();
        PlayerPrefs.SetInt("PowerAmount", PowerAmount);
    }

    private void LoadResources()
    {
        PowerAmount = PlayerPrefs.GetInt("PowerAmount", 0);
        txt_Power.text = PowerAmount.ToString();
    }
}
