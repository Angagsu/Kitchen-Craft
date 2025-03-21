using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    private Scene scene;

    [SerializeField] private Button playSingleplayerButton;
    [SerializeField] private Button playMultiplayerButton;
    [SerializeField] private Button playMultiplayerArenaButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playSingleplayerButton.onClick.AddListener(() => 
        {
            KitchenGameMultiplayer.IsPlayMultiplayer = false;
            KitchenGameMultiplayer.IsPlayMultiplayerArenaMode = false;
            Loader.Load(Loader.Scene.LobbyScene);
        });

        playMultiplayerButton.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.IsPlayMultiplayer = true;
            KitchenGameMultiplayer.IsPlayMultiplayerArenaMode = false;
            Loader.Load(Loader.Scene.LobbyScene);
        });

        playMultiplayerArenaButton.onClick.AddListener(() =>
        {
            KitchenGameMultiplayer.IsPlayMultiplayer = true;
            KitchenGameMultiplayer.IsPlayMultiplayerArenaMode = true;
            Loader.Load(Loader.Scene.LobbyScene);
        });

        quitButton.onClick.AddListener(() => { Application.Quit(); });

        Time.timeScale = 1f;
    }

}
