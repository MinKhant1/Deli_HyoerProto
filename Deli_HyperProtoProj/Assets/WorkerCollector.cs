using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerCollector : MonoBehaviour
{

    public int CarryLimit;
    public int CarryNumber;
    public float _currentStackY;

    public List<Food> foodsCarrying = new List<Food>();

    [SerializeField] GameObject collectorParent;





    public void CollectFood(Food food)
    {
        if (!food.Collected)
        {

            food.GoTostack(collectorParent.transform, _currentStackY);
            _currentStackY += food.foodSizeY;
            CarryNumber++;
            foodsCarrying.Add(food);
            food.Collected = true;
        }
    }


    public IEnumerator TransferFoodToCustomer(Customer customer)
    {

        foodsCarrying.Reverse();
        foreach (Food item in foodsCarrying.ToArray())
        {
            foreach (Order order in customer.orders)
            {

                if (item.FoodType == order.OrderedFood && order.NumberOfFood > 0)
                {
                   
                    _currentStackY -= item.foodSizeY;
                    customer.ValideOrder(item.FoodType);
                    CarryNumber--;
                    foodsCarrying.Remove(item);
                    item.GoToCustomer(customer.gameObject.transform);
                    //SoundManager.Instance.PlaySoundAndVibrate(_collectClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }

        }
    }
}
