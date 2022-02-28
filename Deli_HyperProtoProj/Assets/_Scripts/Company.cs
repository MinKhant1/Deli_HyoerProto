using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Company : MonoBehaviour
{
    [SerializeField] GameObject _companyUI;
    [SerializeField] GameObject companyTarget;
    [SerializeField] WorkerAI workerPrefab;

    [SerializeField] Worker[] workers=new Worker[2];

    [SerializeField] Image InteractProgressBar;

    Tween _uiTween;
    float _interactBarFill;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
           _uiTween= DOVirtual.Float(0, 1, 0.5f, OnUpdateInteractBar).OnComplete(()=>ShowCompanyUi(true));
           
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_uiTween.active) _uiTween.Kill();
            ShowCompanyUi(false);

        }

    }

    public void ShowCompanyUi(bool show)
    {
        
        _companyUI.SetActive(show);
        _interactBarFill = 0;
        InteractProgressBar.fillAmount = 0;



    }


     void OnUpdateInteractBar(float barfloat)
    {
        _interactBarFill = barfloat;
        InteractProgressBar.fillAmount = _interactBarFill;
    }



    public void SpawnWorker(int workerNumber)
    {

        if(!workers[workerNumber].Spawned)
        {
            WorkerAI workerAi = Instantiate(workerPrefab, companyTarget.transform.position, Quaternion.identity);

            workerAi.FoodType = workers[workerNumber].WorkerFood;

            workers[workerNumber].Spawned = true;
        }
        

    }


    [System.Serializable]
    public class Worker
    {
        public Button WorkerButton;
        public FoodType WorkerFood;
        public bool Spawned;

    }

}
