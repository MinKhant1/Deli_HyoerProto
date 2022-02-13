using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public GameObject StackParent;

    public float CurrentStackY;




    public int number;

    Vector3 _position;

    private void Start()
    {
        CurrentStackY = 0f;
        _position = transform.position;
    }

    private void Update()
    {

    }

    public void Collect()
    {
        number++;
        transform.position = _position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Food food))
        {

            if (!food.Collected)
            {
                print("Hi");
                Collect();
                food.GoTostack();
                food.Collected = true;

            }
        }
    }


}
