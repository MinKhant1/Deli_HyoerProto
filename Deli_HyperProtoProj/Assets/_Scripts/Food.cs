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
    }
    public void GoTostack()
    {
        FoodSpawner foodSpawner = transform.parent.GetComponent<FoodSpawner>();

        foodSpawner.Spawned = false;


        _stackY = collector.CurrentStackY;

        //transform.SetParent();
        transform.parent = collector.StackParent.transform;
        transform.localPosition = Vector3.zero;
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMove(new Vector3(0, _stackY + 2f, 0), 0.2f));
        sequence.Append(transform.DOLocalMove(new Vector3(0, _stackY, 0), 0.2f));
        collector.CurrentStackY +=foodSizeY;
    }

    public void GoToCustomer(Transform customer)
    {
       
        transform.DOMove(customer.position, 0.2f).OnComplete(()=>Destroy(gameObject));


    }


}
