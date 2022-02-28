using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{

    Collector collector;
    public bool Collected;

    Tweener _moveToCollector;
    Vector3 _lastCollectorPosition;
    float _stackY;

    IEnumerator SpawnFoodRoutine;



    public FoodType FoodType;

    
   public float foodSizeY=0.8f;


    private void Start()
    {
        collector = FindObjectOfType<Collector>();

        LevelData.Instance.UncollectedFood.Add(this);
    }
    public void GoTostack(Transform collectorParent,float yPosition)
    {
        FoodSpawner foodSpawner = transform.parent.GetComponent<FoodSpawner>();

        foodSpawner.Spawned = false;

        transform.parent = collectorParent;
        transform.localPosition = Vector3.zero;
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMove(new Vector3(0, yPosition + 2f, 0), 0.2f));
        sequence.Append(transform.DOLocalMove(new Vector3(0, yPosition, 0), 0.2f));
        LevelData.Instance.UncollectedFood.Remove(this);

    }

    public void GoToCustomer(Transform customer)
    {
       
        transform.DOMove(customer.position, 0.2f).OnComplete(()=>Destroy(gameObject));


    }


}
