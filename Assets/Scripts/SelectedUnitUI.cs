using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedUnitUI : MonoBehaviour
{
    [SerializeField] public Image UnitSprite;
    [SerializeField] public Image ImgHealthBar;
    [SerializeField] public TextMeshProUGUI txtUnitName;

    public void SelectUnit(Sprite _sprite, float healthAmount, string _unitName)
    {
        UnitSprite.sprite = _sprite;
        ImgHealthBar.fillAmount = healthAmount;
        txtUnitName.text = _unitName;
    }

    public void UpdateHealth(float healthAmount)
    {
        ImgHealthBar.fillAmount = healthAmount;
    }
}
