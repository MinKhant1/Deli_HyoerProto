using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VehicleShop : MonoBehaviour
{

    public VehicleType vehicle;
    int costLeft;
    public bool bought;
    public Transform Target;

    Player player;

    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] GameObject _moneyUI;
    [SerializeField] Image _progressImage;

    public float progress;




    private void Start()
    {
        player = FindObjectOfType<Player>();
        costLeft = vehicle.Cost;
        _moneyText.text = costLeft.ToString();


    }


 


    public void ReceiveMoney()
    {
        costLeft -= 5;

        float p = (float)costLeft / vehicle.Cost;
        progress = 1 - p;

        _progressImage.fillAmount = progress;
        _moneyText.text = costLeft.ToString();
        if(costLeft<=0)
        {
            VehicleBought();
        }
    }

    private void VehicleBought()
    {
        _moneyUI.SetActive(false);
        player.ChangeVehicleModelAndAnimation(vehicle.Name);
        ChangeVehicle(player.gameObject);
        bought = true;
    }

    public void ChangeVehicle(GameObject player)
    {
      
        player.GetComponent<AnimationMovementController>().playerSpeed = vehicle.Speed;
        player.GetComponent<Collector>().carryLimit = vehicle.CarryLimit;
    }
}
