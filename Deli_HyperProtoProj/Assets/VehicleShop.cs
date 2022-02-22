using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleShop : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag(Tags.Player))
        {
            other.transform.Find("Hi").gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                print("Hi");
               

            }
        }
        
    }
}
