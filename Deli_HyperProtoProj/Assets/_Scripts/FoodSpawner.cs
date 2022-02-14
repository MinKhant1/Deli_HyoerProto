using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] GameObject Food;
    public float SpawnInterval=3;

    public bool Spawned;


    private void Start()
    {
        SpawnFood();
        Spawned = true;
        
    }


    private void Update()
    {
        
    


        if(!Spawned)
        {
            StartCoroutine(SpawnFoodInterval());
            Spawned = true;
        }
       
    }


    public IEnumerator SpawnFoodInterval()
    {
        yield return new WaitForSeconds(3);
        SpawnFood();
       
       
    }

    private void SpawnFood()
    {
        GameObject food;
        food= Instantiate(Food, transform.position, Quaternion.identity) as GameObject;
        food.transform.SetParent(transform);

       
    }
}
