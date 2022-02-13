using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Transform CunrrentStackTransform;
    public Transform CollectorObjectposition;

   


    public int number;

  

    public void Collect()
    { 
        number++;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Food food))
        {
            Collect();
            food.GoTostack();
        }
    }


}
