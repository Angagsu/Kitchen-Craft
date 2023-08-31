using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.Netcode;

public class LobbyMessageUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(Hide);
    }

    private void Start()
    {
        KitchenGameMultiplayer.Instance.OnFaildToJoinGame += KitchenGameMultiplayer_OnFaildToJoinGame;
        GameLobby.Instance.OnCreateLobbyStarted += GameLobby_OnCreateLobbyStarted;
        GameLobby.Instance.OnCreateLobbyFailed += GameLobby_OnCreateLobbyFaild;
        GameLobby.Instance.OnJoinStarted += GameLobby_OnJoinStarted;
        GameLobby.Instance.OnJoinFailed += GameLobby_OnJoinFailed;
        GameLobby.Instance.OnQuickJoinFailed += GameLobby_OnQuickJoinFailed;

        Hide();
    }

    private void GameLobby_OnQuickJoinFailed(object sender, System.EventArgs e)
    {
        ShowMessage("Could Not Find Lobby To Quick Join !");
    }

    private void GameLobby_OnJoinFailed(object sender, System.EventArgs e)
    {
        ShowMessage("Failed To Join Lobby !");
    }

    private void GameLobby_OnJoinStarted(object sender, System.EventArgs e)
    {
        ShowMessage("Joining Lobby ...");
    }

    private void GameLobby_OnCreateLobbyFaild(object sender, System.EventArgs e)
    {
        ShowMessage("Faild To Create Lobby !");
    }

    private void GameLobby_OnCreateLobbyStarted(object sender, System.EventArgs e)
    {
        ShowMessage("Creating Lobby ... ");
    }

    private void KitchenGameMultiplayer_OnFaildToJoinGame(object sender, System.EventArgs e)
    {
        if (NetworkManager.Singleton.DisconnectReason == "")
        {
            ShowMessage("Faild To Connect !");
        }
        else
        {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
    }

    private void ShowMessage(string message)
    {
        Show();
        messageText.text = message;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        KitchenGameMultiplayer.Instance.OnFaildToJoinGame -= KitchenGameMultiplayer_OnFaildToJoinGame;
        GameLobby.Instance.OnCreateLobbyStarted -= GameLobby_OnCreateLobbyStarted;
        GameLobby.Instance.OnCreateLobbyFailed -= GameLobby_OnCreateLobbyFaild;
        GameLobby.Instance.OnJoinStarted -= GameLobby_OnJoinStarted;
        GameLobby.Instance.OnJoinFailed -= GameLobby_OnJoinFailed;
        GameLobby.Instance.OnQuickJoinFailed -= GameLobby_OnQuickJoinFailed;
    }
}
