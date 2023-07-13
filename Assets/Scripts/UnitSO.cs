using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Unit")]
public class UnitSO : ScriptableObject
{
    public UnitType unitType;
    public Sprite unitSprite;
    public float unitTrainTime;
    public int unitPrice;
    public int unitHealth;
    public int unitPower;
}

public enum UnitType
{
    Soldier1, Soldier2, Soldier3
}