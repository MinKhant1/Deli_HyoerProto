using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<GameObject> PlayerVehicles=new List<GameObject>();

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.tag==Tags.PlayerVehicle)
            {
                PlayerVehicles.Add(child.gameObject);
            }

        }
    }

    public void ChangeVehicleModelAndAnimation(string vehicleName)
    {
        foreach(GameObject vehicle in PlayerVehicles)
        {
            if(vehicle.name==vehicleName)
            {
                vehicle.SetActive(true);
                GetComponent<AnimationMovementController>().PlayerAnimator = vehicle.GetComponent<Animator>();
            }
            else
            {
                vehicle.SetActive(false);
            }
        }
    }
}
