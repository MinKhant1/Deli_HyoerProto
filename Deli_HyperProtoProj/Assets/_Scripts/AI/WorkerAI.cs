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
    [SerializeField] GameObject Company;
    public GameObject FoodTarget;



    NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _foods = GameObject.FindGameObjectsWithTag(_FoodType.FoodName);



    }

    private void Update()
    {
        foreach (GameObject food in _uncollectedFoods.ToArray())
        {
            if (food.GetComponent<Food>().Collected)
            {
                _uncollectedFoods.Remove(food);
            }

        }
    }

    [Task]
    public bool IsFoodEmpty()
    {
        return FoodTarget == null;
    }

    [Task]
    public void FindFood()
    {
        GameObject closestFood;
        _foods = GameObject.FindGameObjectsWithTag(_FoodType.FoodName);
        foreach (GameObject food in _foods)
        {
            if (!food.GetComponent<Food>().Collected)
            {
                _uncollectedFoods.Add(food);
            }

        }
        closestFood = GetClosestFood(_foods);
        FoodTarget = closestFood;

        ThisTask.Succeed();

    }


    [Task]
    public void GoToFood()
    {
        _agent.SetDestination(FoodTarget.transform.position);


        if (pathComplete())
        {
            ThisTask.Succeed();

        }
        if (FoodTarget == null)
        {
            ThisTask.Fail();

        }


    }

    [Task]
    public void GoToCompany()
    {
        _agent.SetDestination(Company.transform.position);


        if (pathComplete())
        {
            ThisTask.Succeed();
        }

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

    GameObject GetClosestFood(GameObject[] foods)
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
