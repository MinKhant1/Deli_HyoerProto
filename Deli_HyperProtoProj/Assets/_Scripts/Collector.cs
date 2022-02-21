using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class Collector : MonoBehaviour
{
    [Header("Transfering Food")]
    public GameObject StackParent;
    public float CurrentStackY;
    public List<Food> foodsCarrying = new List<Food>();
    public int CarryNumber;
    public int carryLimit;
    Vector3 _position;
    IEnumerator _transferFoodRoutine;
    [SerializeField] GameObject MaxUi;
    


    [Header("Money")]
    public int Money;
    [SerializeField] TextMeshProUGUI _moneyGUI;
    [SerializeField] GameObject _moneyObj;
    IEnumerator _transferMoneyRoutine;
    private void Start()
    {
        CurrentStackY = 0f;
        _position = transform.position;
       
    }

    private void Update()
    {
        if(CarryNumber==carryLimit)
        {
            MaxUi.SetActive(true);
        }
        else
        {
            MaxUi.SetActive(false);
        }
        
    }
    public void Collect()
    {

        CarryNumber++;
        transform.position = _position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (CarryNumber < carryLimit)
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
            _transferFoodRoutine = TransferFoodToCustomer(customer);
            StartCoroutine(_transferFoodRoutine);
        }

        if (other.gameObject.TryGetComponent(out MoneyScript money))
        {
            AddMoney(money.Amount);
            Destroy(other.gameObject);
        }


        if (other.gameObject.TryGetComponent(out TileUnlocker tileUnlocker))
        {
            _transferMoneyRoutine = TransferMoney(tileUnlocker);
           StartCoroutine(_transferMoneyRoutine);



        }



    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Customer"))
        {
            foodsCarrying.Reverse();
            if (_transferFoodRoutine != null)
                StopCoroutine(_transferFoodRoutine);
        }
        if(other.CompareTag("TileUnlocker"))
        {
            if(_transferMoneyRoutine!=null)
            {
                StopCoroutine(_transferMoneyRoutine);
            }
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
                    item.GoToCustomer(customer.gameObject.transform);
                    CurrentStackY -= item.foodSizeY;
                    customer.ValideOrder(item.FoodType);
                    CarryNumber--;
                    foodsCarrying.Remove(item);
                    yield return new WaitForSeconds(0.2f);
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
            _moneyGUI.text = Money.ToString();
            yield return new WaitForSeconds(0.2f);

        }
    }

    public void AddFood(Food food)
    {
        foodsCarrying.Add(food);

    }

    public void AddMoney(int amount)
    {
        Money += amount;
        _moneyGUI.text = Money.ToString();
    }

}
