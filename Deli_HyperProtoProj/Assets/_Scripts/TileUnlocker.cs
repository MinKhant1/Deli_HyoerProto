using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileUnlocker : MonoBehaviour
{
    [HideInInspector]
    public int Cost;

    [HideInInspector]
    public int CostLeft;
    [HideInInspector]
    public GameObject TileToUnlock;
    [HideInInspector]
    public GameObject TileBank;

    [HideInInspector]
    public GameObject UIObject;
    TextMeshProUGUI moneyText;

    public Transform Target;

   public bool TileUnlocked;

   


    private void Start()
    {
        UIObject = transform.GetChild(0).gameObject;
        moneyText = UIObject.GetComponentInChildren<TextMeshProUGUI>();

        CostLeft = Cost;
        moneyText.text = CostLeft.ToString();
       

    }

    public void ReceiveMoney()
    {
        CostLeft -= 5;
        moneyText.text = CostLeft.ToString();
        if (CostLeft<=0)
        {
            UnlockTile();
            TileUnlocked = true;
        }
       

    }
    public void UnlockTile()
    {
        TileToUnlock.SetActive(true);
        TileBank.SetActive(false);
        GetComponent<Collider>().enabled = false;
        UIObject.SetActive(false);
    }




}
