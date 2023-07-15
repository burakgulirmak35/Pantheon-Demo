using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedUnitUI : MonoBehaviour
{
    [SerializeField] public Image UnitSprite;
    [SerializeField] public Image ImgHealthBar;

    public void SelectUnit(Sprite _sprite, float healthAmount)
    {
        UnitSprite.sprite = _sprite;
        ImgHealthBar.fillAmount = healthAmount;
    }

    public void UpdateHealth(float healthAmount)
    {
        ImgHealthBar.fillAmount = healthAmount;
    }
}
