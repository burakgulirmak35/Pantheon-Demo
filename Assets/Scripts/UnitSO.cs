using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Unit")]
public class UnitSO : ScriptableObject
{
    public int unitID;
    public Sprite unitSprite;
    public float unitTrainTime;
    public int unitPrice;
    public int unitHealth;
    public int unitPower;
    public int fireRate;
}