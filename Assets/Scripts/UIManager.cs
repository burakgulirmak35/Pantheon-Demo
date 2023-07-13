using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PrefabsSO prefabs;
    [SerializeField] private SettingsSO settings;
    [Header("Resources")]
    [SerializeField] private TextMeshProUGUI txtPower;
    private int ResourceAmount;
    [Header("Buttons")]
    [SerializeField] private Image imgPowerPlantSelected;
    [SerializeField] private Image imgBarracksSelected;
    [Space]
    [SerializeField] private TextMeshProUGUI txtPowerPlantPrice;
    [SerializeField] private TextMeshProUGUI txtBarracksPrice;
    [Header("Information")]
    [SerializeField] public GameObject imgbgInfo;
    [SerializeField] public Image imgInfo;
    [SerializeField] public GameObject UnitsArea;
    [SerializeField] public TextMeshProUGUI txtBuildingName;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        LoadResources();
        SetButtons();
        RefreshInfo();
    }

    private void SetButtons()
    {
        txtPowerPlantPrice.text = "$" + settings.PowerPlantBuildPrice.ToString();
        txtBarracksPrice.text = "$" + settings.BarracksBuildPrice.ToString();
    }

    public void AddResource(int amount)
    {
        ResourceAmount += amount;
        txtPower.text = ResourceAmount.ToString();
        PlayerPrefs.SetInt("ResourceAmount", ResourceAmount);
    }

    public bool SpendResource(int amount)
    {
        if (ResourceAmount >= amount)
        {
            ResourceAmount -= amount;
            txtPower.text = ResourceAmount.ToString();
            PlayerPrefs.SetInt("ResourceAmount", ResourceAmount);
            return true;
        }
        return false;
    }

    private void LoadResources()
    {
        ResourceAmount = PlayerPrefs.GetInt("ResourceAmount", 50);
        txtPower.text = ResourceAmount.ToString();
    }

    public void btn_PowerPlant()
    {
        if (ResourceAmount >= settings.PowerPlantBuildPrice)
        {
            imgPowerPlantSelected.enabled = true;
            imgBarracksSelected.enabled = false;
            BuildingManager.Instance.SelectPowerPlant();
        }
    }

    public void btn_Barracks()
    {
        if (ResourceAmount >= settings.BarracksBuildPrice)
        {
            imgPowerPlantSelected.enabled = false;
            imgBarracksSelected.enabled = true;
            BuildingManager.Instance.SelectBarrracks();
        }
    }

    public void UnSelectBuildingType()
    {
        imgPowerPlantSelected.enabled = false;
        imgBarracksSelected.enabled = false;
        BuildingManager.Instance.UnSelect();
    }

    public void SelectBuilding(BuildingType buildingType)
    {
        RefreshInfo();
        imgbgInfo.SetActive(true);
        switch (buildingType)
        {
            case BuildingType.Barracks:
                imgInfo.sprite = prefabs.BarracksSprite;
                UnitsArea.SetActive(true);
                txtBuildingName.text = buildingType.ToString();
                break;
            case BuildingType.PowerPlant:
                imgInfo.sprite = prefabs.PowerPlantSprite;
                txtBuildingName.text = buildingType.ToString();
                break;
        }
    }

    private void RefreshInfo()
    {
        imgbgInfo.SetActive(false);
        UnitsArea.SetActive(false);
        txtBuildingName.text = "";
    }
}
