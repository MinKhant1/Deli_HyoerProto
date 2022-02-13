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

    private void Start()
    {
        collector = FindObjectOfType<Collector>();
    }



    private void Update()
    {
        
    }

   

    public void GoTostack()
    {

        collector.Collect();

        _stackY = collector.CurrentStackY;
      
        transform.SetParent(collector.StackParent.transform);
        print(_stackY);
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(new Vector3(0, _stackY+2f , 0), 0.2f));
        sequence.Append(transform.DOLocalMove(new Vector3(0, _stackY, 0), 0.2f));
        collector.CurrentStackY += 0.5f;
        //transform.DOLocalMove(new Vector3(0, _stackY+2f, 0), 0.2f).OnComplete(()=> transform.DOLocalMove(new Vector3(0, _stackY-2f, 0), 0.2f).OnComplete(()=>DOTween.Kill(transform)));


    }
}
