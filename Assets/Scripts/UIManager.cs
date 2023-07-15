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
    [SerializeField] private GameObject PanelBarracks;
    [SerializeField] private GameObject PanelPowerPlant;
    [SerializeField] private GameObject PanelSelectedUnits;
    [Header("SelectedUnits")]
    [SerializeField] private TextMeshProUGUI txtSelectedCount;
    [SerializeField] private List<SelectedUnitUI> SelectedUnitList = new List<SelectedUnitUI>();

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        LoadResources();
        SetButtons();

        PanelBarracks.SetActive(false);
        PanelPowerPlant.SetActive(false);
        PanelSelectedUnits.SetActive(false);
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
            GridBuildingSystem.Instance.SelectToBuild(BuildingType.PowerPlant);
        }
    }

    public void btn_Barracks()
    {
        if (ResourceAmount >= settings.BarracksBuildPrice)
        {
            imgPowerPlantSelected.enabled = false;
            imgBarracksSelected.enabled = true;
            GridBuildingSystem.Instance.SelectToBuild(BuildingType.Barracks);
        }
    }

    public void UnSelectBuildingType()
    {
        imgPowerPlantSelected.enabled = false;
        imgBarracksSelected.enabled = false;
    }

    public void SelectBarracks()
    {
        PanelPowerPlant.SetActive(false);
        PanelBarracks.SetActive(true);
        PanelSelectedUnits.SetActive(false);
    }

    public void SelectPowerPlant()
    {
        PanelBarracks.SetActive(false);
        PanelPowerPlant.SetActive(true);
        PanelSelectedUnits.SetActive(false);
    }

    public void SelectUnits()
    {
        PanelBarracks.SetActive(false);
        PanelPowerPlant.SetActive(false);
        PanelSelectedUnits.SetActive(true);
    }

    public SelectedUnitUI SelectUnit(int id)
    {
        txtSelectedCount.text = id.ToString();
        return SelectedUnitList[id - 1];
    }
}
