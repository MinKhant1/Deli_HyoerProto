using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleShop : MonoBehaviour
{

    public VehicleType vehicle;


    private void OnTriggerEnter(Collider other)
    {
       if(other.TryGetComponent(out Player player))
        {
            player.ChangeVehicleModel("Hi");
        }
        
    }

    public void ChangeVehicle(GameObject player)
    {
        player.GetComponent<AnimationMovementController>().playerSpeed = vehicle.Speed;
        player.GetComponent<Collector>().carryLimit = vehicle.CarryLimit;
    }
}
