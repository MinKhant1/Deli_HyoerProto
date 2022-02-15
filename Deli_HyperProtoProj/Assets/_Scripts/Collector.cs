using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public GameObject StackParent;

    public float CurrentStackY;

    public List<Food> foodsCarrying = new List<Food>();




    public int number;

    Vector3 _position;

    IEnumerator TransferFood;


    private void Start()
    {
        CurrentStackY = 0f;
        _position = transform.position;
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


        if (other.gameObject.TryGetComponent(out Customer customer))
        {
            TransferFood = TransferFoodToCustomer(customer);
            StartCoroutine(TransferFood);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Customer"))
        {

            if (TransferFood != null)
                StopCoroutine(TransferFood);
        }

    }

    private void OnTriggerStay(Collider other)
    {
    }


    public IEnumerator TransferFoodToCustomer(Customer customer)
    {


        //foreach (Food item in foodsCarrying.ToArray())
        //{
        //    foreach (Order order in customer.orders)
        //    {

        //        if (item.FoodType == order.OrderedFood)
        //        {
        //            item.GoToCustomer(customer.gameObject.transform);
        //            CurrentStackY -= item.foodSizeY;
        //            foodsCarrying.Remove(item);
        //            yield return new WaitForSeconds(0.1f);
        //        }
        //    }

        //}

        for (int i = foodsCarrying.ToArray().Length-1; i >= 0; i--)
        {
           
            foreach (Order order in customer.orders)
            {
                if (foodsCarrying[i].FoodType == order.OrderedFood)
                {
                    foodsCarrying[i].GoToCustomer(customer.gameObject.transform);
                    //customer.ValideOrder(foodsCarrying[i].FoodType);
                    CurrentStackY -= foodsCarrying[i].foodSizeY;
                    //foodsCarrying.Remove(foodsCarrying[i]);
                   
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
    }

    public void AddFood(Food food)
    {
        foodsCarrying.Add(food);

    }

}
