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

    private void Start()
    {
        collector = FindObjectOfType<Collector>();
    }
    public void GoTostack()
    {
        FoodSpawner foodSpawner=transform.parent.GetComponent<FoodSpawner>();

        foodSpawner.Spawned = false;
     

        _stackY = collector.CurrentStackY;

        transform.SetParent(collector.StackParent.transform, true);

        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(new Vector3(0, _stackY + 2f, 0), 0.2f));
        sequence.Append(transform.DOLocalMove(new Vector3(0, _stackY, 0), 0.2f));
        collector.CurrentStackY += 0.8f;

      


    }


}
