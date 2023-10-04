using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour, ISavable
{
    public int deathCount;
    public float healt;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            deathCount++;
            healt++;
            MyDataSaveManager.Instance.SaveGame();
            Debug.Log("Try To Save !!!");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            MyDataSaveManager.Instance.Delete();
        }
    }
    public void LoadData(MyGameData data)
    {
        //transform.position = data.PlayerPosition;
        healt = data.Health;
        deathCount = data.DeathCount;
    }

    public void SaveData(ref MyGameData data)
    {
        //data.PlayerPosition = transform.position;
        data.Health = healt;
        data.DeathCount = deathCount;
        Debug.Log("healt = " + healt);
        Debug.Log("deathCount = " + deathCount);
    }
}
