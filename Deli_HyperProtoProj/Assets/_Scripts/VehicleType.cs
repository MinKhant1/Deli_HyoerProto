using UnityEngine;


[CreateAssetMenu(fileName = "New Vehicle", menuName = "Vehicles/Create Vehicle")]

public class VehicleType : ScriptableObject
{
    public string Name;
    public float Speed;
    public int CarryLimit;
    public int Cost;    
   
}
