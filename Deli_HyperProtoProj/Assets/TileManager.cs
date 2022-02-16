using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<TileData> TileDatas = new List<TileData>();

    private void Start()
    {
        foreach (TileData data in TileDatas)
        {
            data.Unlocker.TileToUnlock = data.Tile;
            data.Unlocker.Cost = data.CostToUnlock;
        }
    }


}

[System.Serializable]
public class TileData
{
    public GameObject Tile;
    public int CostToUnlock;
    public TileUnlocker Unlocker;


}


