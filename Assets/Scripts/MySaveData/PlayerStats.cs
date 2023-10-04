using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour , ISavable
{
    private string playerName;
    private int coinCount;
    private int dimondCount;
    
    
    void Start()
    {
        playerName = "Gago";
        coinCount = 10;
        dimondCount = 34;
    }

    void Update()
    {
        
    }
    public void LoadData(MyGameData data)
    {
        playerName = data.PlayerName;
        coinCount = data.CoinCount;
        dimondCount = data.DimondCount;
    }

    public void SaveData(ref MyGameData data)
    {
        data.PlayerName = playerName;
        data.CoinCount = coinCount;
        data.DimondCount = dimondCount;
    }
}
