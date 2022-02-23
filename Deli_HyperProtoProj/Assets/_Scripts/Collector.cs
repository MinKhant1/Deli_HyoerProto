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
    IEnumerator _transferMoneyToTileUnlockRoutine;
    IEnumerator _transferMoneyToVehicleShopRoutine;



    [Header("SFX")]
    [SerializeField] AudioClip _collectClip;
    private void Start()
    {
        CurrentStackY = 0f;
        _position = transform.position;

        _moneyGUI.text = Money.ToString();
       
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
        SoundManager.Instance.PlaySoundAndVibrate(_collectClip);
     
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
            AddOrRemoveMoney(money.Amount);
            Destroy(other.gameObject);
        }
        if (other.gameObject.TryGetComponent(out TileUnlocker tileUnlocker))
        {
            _transferMoneyToTileUnlockRoutine = TransferMoneyToTileUnlocker(tileUnlocker);
           StartCoroutine(_transferMoneyToTileUnlockRoutine);

        }

        if (other.gameObject.TryGetComponent(out VehicleShop vehicleShop))
        {
            _transferMoneyToVehicleShopRoutine = TransferMoneyToVehicleShop(vehicleShop);
            StartCoroutine(_transferMoneyToVehicleShopRoutine);
        }






    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag(Tags.Customer))
        {
            foodsCarrying.Reverse();
            if (_transferFoodRoutine != null)
            {
                StopCoroutine(_transferFoodRoutine);
                _transferFoodRoutine = null;

            }
           
        }
        if(other.CompareTag(Tags.TileUnlocker))
        {
            if(_transferMoneyToTileUnlockRoutine!=null)
            {
                StopCoroutine(_transferMoneyToTileUnlockRoutine);
                _transferMoneyToTileUnlockRoutine = null;
            }
        }
        if(other.CompareTag(Tags.VehicleShop))
        {
            if(_transferMoneyToVehicleShopRoutine!=null)
            {
                StopCoroutine(_transferMoneyToVehicleShopRoutine);
                _transferMoneyToVehicleShopRoutine = null;
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
                    SoundManager.Instance.PlaySoundAndVibrate(_collectClip);
                    yield return new WaitForSeconds(0.2f);
                }
            }

        }


    }

    public IEnumerator TransferMoneyToTileUnlocker(TileUnlocker tileUnlocker)
    {
        while (Money > 0 && !tileUnlocker.TileUnlocked)
        {
            GameObject money = Instantiate(_moneyObj, transform.position, Quaternion.identity);
            money.transform.DOMove(tileUnlocker.Target.position, .2f).OnComplete(()=>Destroy(money.gameObject));
            tileUnlocker.ReceiveMoney();
            AddOrRemoveMoney(-5);
            SoundManager.Instance.PlaySoundAndVibrate(_collectClip);
            yield return new WaitForSeconds(0.2f);

        }
    }

    public IEnumerator TransferMoneyToVehicleShop(VehicleShop shop)
    {
        while(Money>0 && !shop.bought)
        {

            GameObject money = Instantiate(_moneyObj, transform.position, Quaternion.identity);
            money.transform.DOMove(shop.Target.position, .2f).OnComplete(() => Destroy(money.gameObject));
            shop.ReceiveMoney();
            AddOrRemoveMoney(-5);
            yield return new WaitForSeconds(0.05f);
        }

    }


    public void AddFood(Food food)
    {
        foodsCarrying.Add(food);

    }

    public void AddOrRemoveMoney(int amount)
    {
        Money += amount;
        _moneyGUI.text = Money.ToString();
    }

}
