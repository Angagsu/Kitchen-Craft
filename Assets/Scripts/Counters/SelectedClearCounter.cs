using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedClearCounter : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;
    private void Start()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
        }
        else
        {
            Player.OnAnyPlayerSpawned += Player_OnAnyPlayerSpawned;
        }
    }

    private void Player_OnAnyPlayerSpawned(object sender, System.EventArgs e)
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.OnSelectedCounterChanged -= PlayerOnSelectedCounterChanged;
            Player.LocalInstance.OnSelectedCounterChanged += PlayerOnSelectedCounterChanged;
        }
    }

    private void PlayerOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.SelectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (var visualGameObject in visualGameObjects)
        {
            visualGameObject.gameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (var visualGameObject in visualGameObjects)
        {
            visualGameObject.gameObject.SetActive(false);
        }
    }
}
