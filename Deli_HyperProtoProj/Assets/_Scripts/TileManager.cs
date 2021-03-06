using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<TileData> TileDatas = new List<TileData>();

    private void Awake()
    {
        foreach (TileData data in TileDatas)
        {
            data.Unlocker.TileToUnlock = data.Tile;
            data.Unlocker.Cost = data.CostToUnlock;
            data.Unlocker.TileBanks = data.TileBanks;
        }
    }
  


}

[System.Serializable]
public class TileData
{
    public GameObject Tile;
    public GameObject[] TileBanks;
    public int CostToUnlock;
    public TileUnlocker Unlocker;


}


