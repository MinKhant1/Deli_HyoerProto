using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Food : MonoBehaviour
{

    Collector collector;
    bool _collected;

    Tweener _moveToCollector;

    Vector3 _lastCollectorPosition;

    float _stackY;

    private void Start()
    {
        collector = FindObjectOfType<Collector>();
    }



    private void Update()
    {
        _stackY = collector.number * 0.3f;
    }

   

    public void GoTostack()
    {
     
        transform.SetParent(collector.CunrrentStackTransform);
       
        transform.DOLocalMove(new Vector3(0, _stackY*2f, 0), 0.2f).OnComplete(()=> transform.DOLocalMove(new Vector3(0, _stackY, 0), 0.2f));
     
    }
}
