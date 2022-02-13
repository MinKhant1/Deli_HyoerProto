using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public GameObject StackParent;

    public float CurrentStackY;

    public List<Food> foodsCarrying=new List<Food>();




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
               
                Collect();
                AddFood(food);
                food.GoTostack();
                food.Collected = true;

            }
        }
    }

    public void AddFood(Food food)
    {
        foodsCarrying.Add(food);

    }

}
