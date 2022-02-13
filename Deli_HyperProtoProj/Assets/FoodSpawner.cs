using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] GameObject Food;
    public float SpawnInterval=3;



    private void Start()
    {
        SpawnFood();
        
    }


    public IEnumerator SpawnFoodInterval()
    {
        SpawnFood();
        yield return new WaitForSeconds(3);
    }

    private void SpawnFood()
    {
        Instantiate(Food, transform.position, Quaternion.identity);
    }
}
