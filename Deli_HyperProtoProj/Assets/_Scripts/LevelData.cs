using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData Instance;
 
    public List<Food> UncollectedFood = new List<Food>();


   
    public List<Customer> customers = new List<Customer>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
