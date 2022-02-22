using UnityEngine;

[ CreateAssetMenu(fileName ="New Food", menuName ="Foods/Create Food")]
public class FoodType :ScriptableObject
{
    public string FoodName;
    public Sprite FoodImage;
    public float Price;


}
