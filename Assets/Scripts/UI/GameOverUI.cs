using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button playAgainButton;
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI leftTeamRecipesDeliveredText;
    [SerializeField] private TextMeshProUGUI rightTeamRecipesDeliveredText;

    [SerializeField] private DeliveryCounter leftTeamDeliveredRecipes;
    [SerializeField] private DeliveryCounter rightTeamDeliveredRecipes;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.Shutdown();
            Loader.Load(Loader.Scene.MainMenuScene);
        }); 
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            if (leftTeamDeliveredRecipes && rightTeamDeliveredRecipes)
            {
                leftTeamRecipesDeliveredText.text = leftTeamDeliveredRecipes.GetSuccessfulRecipesAmount().ToString();
                rightTeamRecipesDeliveredText.text = rightTeamDeliveredRecipes.GetSuccessfulRecipesAmount().ToString();
            }
            else
            {            
                recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmount().ToString();
            }
        }
        else
        {
            Hide();
        }
        
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);

    }
}

