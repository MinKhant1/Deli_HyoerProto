using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TileUnlocker : MonoBehaviour
{
    public int Cost;
    public GameObject TileToUnlock;

    public GameObject UIObject;


    private void Start()
    {
        UIObject = transform.GetChild(0).gameObject;

    }




}
