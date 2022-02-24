using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class WorkerAI : MonoBehaviour
{

    public GameObject[] _foods;
    public List<GameObject> _uncollectedFoods = new List<GameObject>();



    [SerializeField] FoodType _FoodType;

    public GameObject FoodTarget;



    public Customer currentCustomer;



    Animator _anim;
    NavMeshAgent _agent;

    public WorkerCollector _collector;

    IEnumerator transferFoodToCustomerRoutine;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        _collector = GetComponent<WorkerCollector>();
        _foods = GameObject.FindGameObjectsWithTag(_FoodType.FoodName);




    }

    private void Update()
    {
        foreach (GameObject food in _uncollectedFoods.ToArray())
        {
            if (food.GetComponent<Food>().Collected)
            {
                _uncollectedFoods.Remove(food);
                FoodTarget = null;
            }

        }

        if (_agent.velocity != Vector3.zero)
        {
            _anim.SetBool("IsRunning", true);

        }
        else
        {
            _anim.SetBool("IsRunning", false);
        }

    }


    [Task]
    public void FindFood()
    {
        AgentStop();
        GameObject closestFood;
        _foods = GameObject.FindGameObjectsWithTag(_FoodType.FoodName);
        foreach (GameObject food in _foods)
        {
            if (!_uncollectedFoods.Contains(food))
            {


                if (!food.GetComponent<Food>().Collected && food != null)
                {

                    _uncollectedFoods.Add(food);
                }
            }

        }
        closestFood = GetClosestFood(_uncollectedFoods);
        FoodTarget = closestFood;

        ThisTask.Succeed();

    }


    [Task]
    public void GoToFood()
    {
        if (FoodTarget == null)
        {
            ThisTask.Fail();

        }
        else
        {
            AgentMove(FoodTarget.transform.position);

        }

        if (pathComplete())
        {
            if (FoodTarget != null)
            {
                Food food = FoodTarget.GetComponent<Food>();
                _collector.CollectFood(food);

            }
            if (IsFoodFull())
            {
                ThisTask.Succeed();

            }

        }



    }

    //[Task]
    //public bool CustomerFound()
    //{
    //    return currentCustomer != null;
    //}
    //[Task]
    [Task]
    public void IsCustomerFound()
    {
        ThisTask.Complete(currentCustomer);
    }

    [Task]
    public void FindCustomer()
    {
        currentCustomer = FindObjectOfType<Customer>();

     
        if (currentCustomer != null)
        {
            ThisTask.Succeed();
        }
        else
        {
            ThisTask.Fail();
        }
    }

    [Task]
    public void GoToCustomer()
    {

        if (currentCustomer == null || currentCustomer.OrderComplete)
        {
            AgentStop();
            ThisTask.Fail();

        }
        else
        {
            AgentMove(currentCustomer.transform.position);
        }



        if (pathComplete())
        {

            ThisTask.Succeed();

        }

    }

    private void AgentMove(Vector3 position)
    {
        _agent.isStopped = false;
        _agent.SetDestination(position);
        _anim.SetBool("IsRunning", true);
    }

    void AgentStop()
    {
        _agent.isStopped = true;
        _anim.SetBool("IsRunning", false);
    }

    [Task]
    public void TransferFood()
    {


        if (currentCustomer == null || currentCustomer.OrderComplete)
        {
            _agent.isStopped = true;
            ThisTask.Fail();

        }

        if(_collector.CarryNumber<=0)
        {
            ThisTask.Succeed();
        }
     
        transferFoodToCustomerRoutine = _collector.TransferFoodToCustomer(currentCustomer);
        StartCoroutine(transferFoodToCustomerRoutine);

        
    }

    [Task]
    public bool IsFoodFull()
    {
        return _collector.CarryNumber >= _collector.CarryLimit;
        //ThisTask.Complete(_collector.CarryNumber >= _collector.CarryLimit);

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

    GameObject GetClosestFood(List<GameObject> foods)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in foods)
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

}
