using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
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
    [SerializeField] GameObject _moneyObj;

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
        if (Number < carryLimit)
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


        if (other.gameObject.TryGetComponent(out TileUnlocker tileUnlocker))
        {

           StartCoroutine( TransferMoney(tileUnlocker));



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

                if (item.FoodType == order.OrderedFood && order.NumberOfFood > 0)
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

    public IEnumerator TransferMoney(TileUnlocker tileUnlocker)
    {
        while (Money > 0 && !tileUnlocker.TileUnlocked)
        {
            GameObject money = Instantiate(_moneyObj, transform.position, Quaternion.identity);
            money.transform.DOMove(tileUnlocker.Target.position, .2f).OnComplete(()=>Destroy(money.gameObject));
            tileUnlocker.ReceiveMoney();
            Money -= 5;
            yield return new WaitForSeconds(0.1f);

        }
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
