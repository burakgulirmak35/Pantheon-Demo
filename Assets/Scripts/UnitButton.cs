using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitButton : MonoBehaviour
{
    [SerializeField] private PrefabsSO prefabs;
    [SerializeField] private SettingsSO settings;
    [SerializeField] private UnitSO unitSO;
    [Space]
    [SerializeField] private Image imgUnit;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [Space]
    [SerializeField] private TextMeshProUGUI txtHealth;
    [SerializeField] private TextMeshProUGUI txtPower;

    private void Awake()
    {
        imgUnit.sprite = unitSO.unitSprite;
        txtPrice.text = "$" + unitSO.unitPrice.ToString();
        txtHealth.text = unitSO.unitHealth.ToString();
        txtPower.text = unitSO.unitPower.ToString();
    }

    private void buyUnit()
    {
        if (UIManager.Instance.SpendResource(unitSO.unitPrice))
        {

        }
    }
}
