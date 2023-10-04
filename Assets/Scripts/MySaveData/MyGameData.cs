
using UnityEngine;

[System.Serializable]
public class MyGameData
{
    public float Health;
    public int DeathCount;
    public string PlayerName;
    public int CoinCount;
    public int DimondCount;
    //public Vector3 PlayerPosition;
    //public SerializableDictionary<string, bool> coinsCollected;


    public MyGameData()
    {
        Health = 100;
        DeathCount = 0;
        PlayerName = "Player Name";
        CoinCount = 0;
        DimondCount = 0;
        //PlayerPosition = Vector3.zero;
        //coinsCollected = new SerializableDictionary<string, bool>();
    }
}