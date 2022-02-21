using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{

    public List<Order> orders = new List<Order>();
    
    [SerializeField]
    GameObject _foodUi;
    [SerializeField]
    GameObject _customerUiParent;

    bool _orderComplete;



    [SerializeField]
    GameObject money;



    public float Payment;
    bool finish;
    NavMeshAgent _agent;





    private void Start()
    {
        MakeOrder();
    }


    private void Update()
    {

        _orderComplete = OrderDone();
       if(_orderComplete)
        {
           
            if(!finish)
            {
                Finish();
                finish = true;
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

    public void Finish()
    {
         GameObject moneyScript= Instantiate(money, transform.position, Quaternion.identity);
         moneyScript.GetComponent<MoneyScript>().Amount = 25;
        
    }

    public GameObject FindNearestExit()
    {
        GameObject[] exits = GameObject.FindGameObjectsWithTag(Tags.CustomerExit);
      
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in exits)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;


    }

    public bool pathComplete()
    {
        if (Vector3.Distance(_agent.destination, _agent.transform.position) <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                return true;
            }
        }


        return false;
    }

}

[System.Serializable]
public class Order
{
    public FoodType OrderedFood;

    public int NumberOfFood;

    public GameObject orderUi;

}
