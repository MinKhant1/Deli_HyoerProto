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

    public bool OrderComplete;



    private void Start()
    {
        MakeOrder();
    }


    private void Update()
    {

    }

    public void MakeOrder()
    {
        foreach (Order order in orders)
        {
            GameObject foodui = Instantiate(_foodUi, _customerUiParent.transform);
            foodui.GetComponentInChildren<Image>().sprite = order.OrderedFood.FoodImage;
            foodui.GetComponentInChildren<TextMeshProUGUI>().text = "X" + order.NumberOfFood;

        }
    }
    //public void ValideOrder(FoodType food)
    //{
    //    for (int i = 0; i < orders.Count - 1; i++)
    //    {

    //        if (orders[i].OrderedFood == food)
    //        {
    //            orders[i].NumberOfFood--;
    //        }
    //    }

    //}





}

[System.Serializable]
public class Order
{
    public FoodType OrderedFood;
    public int NumberOfFood;

}
