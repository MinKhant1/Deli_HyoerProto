using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Customer : MonoBehaviour
{

    public List<Order> orders = new List<Order>();
    
    [SerializeField]
    GameObject _foodUi;
    [SerializeField]
    GameObject _customerUiParent;



    [SerializeField]
    GameObject money;

    public float Payment;

     bool paid;



    private void Start()
    {
        MakeOrder();
    }


    private void Update()
    {
       if(OrderDone())
        {
           
            if(!paid)
            {
                Pay();
                paid = true;
            }
        }
    }

    public void MakeOrder()
    {
       
        foreach (Order order in orders)
        {
           
            GameObject foodui = Instantiate(_foodUi, _customerUiParent.transform);
            foodui.GetComponentInChildren<Image>().sprite = order.OrderedFood.FoodImage;
            foodui.GetComponentInChildren<TextMeshProUGUI>().text = order.NumberOfFood.ToString();
            order.orderUi = foodui;

        }
        
    }
    public void ValideOrder(FoodType food)
    {
        for (int i = 0; i <= orders.Count - 1; i++)
        {
            if (orders[i].OrderedFood == food)
            {

             orders[i].NumberOfFood--;
              orders[i].orderUi.GetComponentInChildren<TextMeshProUGUI>().text =orders[i].NumberOfFood.ToString();
            }
        }

    }

    public bool OrderDone()
    {
        foreach (Order order in orders.ToArray())
        {
            if (order.NumberOfFood > 0)
            {
                return false;
            }
            else
            {
                orders.Remove(order);
                Destroy(order.orderUi);

            }
        }
        return true;
    }

    public void Pay()
    {
         GameObject moneyScript= Instantiate(money, transform.position, Quaternion.identity);
        moneyScript.GetComponent<MoneyScript>().Amount = 25;
        Destroy(gameObject);
    }

}

[System.Serializable]
public class Order
{
    public FoodType OrderedFood;

    public int NumberOfFood;

    public GameObject orderUi;

}
