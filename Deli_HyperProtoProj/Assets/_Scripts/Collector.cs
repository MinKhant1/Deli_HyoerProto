using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Collector : MonoBehaviour
{
    public GameObject StackParent;
    public float CurrentStackY;
    public List<Food> foodsCarrying = new List<Food>();




    public int Number;
    public int carryLimit;

   

    Vector3 _position;

    IEnumerator TransferFood;


    public int Money;
    [SerializeField] TextMeshProUGUI _moneyGUI;

    private void Start()
    {
        CurrentStackY = 0f;
        _position = transform.position;
    }
    public void Collect()
    {

        Number++;
        transform.position = _position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(Number<=carryLimit)
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

        if (other.gameObject.TryGetComponent(out MoneyScript money))
        {
            SetMoney(money.Amount);
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
       
        if (other.CompareTag("Customer"))
        {
            foodsCarrying.Reverse();
            if (TransferFood != null)
                StopCoroutine(TransferFood);
        }

    }

    private void OnTriggerStay(Collider other)
    {
    }


    public IEnumerator TransferFoodToCustomer(Customer customer)
    {

        foodsCarrying.Reverse();
        foreach (Food item in foodsCarrying.ToArray())
        {
            foreach (Order order in customer.orders)
            {

                if (item.FoodType == order.OrderedFood && order.NumberOfFood>0)
                {
                    item.GoToCustomer(customer.gameObject.transform);
                    CurrentStackY -= item.foodSizeY;
                    customer.ValideOrder(item.FoodType);
                    Number--;
                    foodsCarrying.Remove(item);
                    yield return new WaitForSeconds(0.1f);
                }
            }

        }

       
    }

    public IEnumerator TransferMoney()
    {
        yield return new WaitForSeconds(0.1f);
    }

    public void AddFood(Food food)
    {
        foodsCarrying.Add(food);

    }

    public void SetMoney(int amount)
    {
        Money += amount;
        _moneyGUI.text = Money.ToString();
    }

}
