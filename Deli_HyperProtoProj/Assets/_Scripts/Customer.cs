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
    [SerializeField]
    GameObject _deliveryPlaceUi;
   public bool OrderComplete;



    [SerializeField]
    GameObject money;
    [SerializeField]
    Transform moneySpawnPoint;



    public float Payment;
    bool finish;
    NavMeshAgent _agent;
    Animator _anim;



    public CustomerSpawner customerSpawner;



    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        MakeOrder();


    }


    private void Update()
    {

        OrderComplete = OrderDone();
        if (OrderComplete && !finish)
        {

            Finish();
            finish = true;



        }

        if (OrderComplete && pathComplete())
        {

            if (customerSpawner != null)
            {
                customerSpawner.Spawned = false;

            }
            Destroy(gameObject);
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
                orders[i].orderUi.GetComponentInChildren<TextMeshProUGUI>().text = orders[i].NumberOfFood.ToString();
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
        GameObject moneyScript = Instantiate(money, moneySpawnPoint.position, Quaternion.identity);
        moneyScript.GetComponent<MoneyScript>().Amount = 25;

        _customerUiParent.SetActive(false);
        _deliveryPlaceUi.SetActive(false);

        _anim.SetBool("Walk", true);
        _agent.SetDestination(FindNearestExit().transform.position);

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
