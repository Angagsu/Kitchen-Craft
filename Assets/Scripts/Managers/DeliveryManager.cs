using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;

public class DeliveryManager : NetworkBehaviour
{
    public static DeliveryManager Instance { get; private set; }


    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;


    [SerializeField] private RecipeSOList recipeSOList;

    private List<RecipeSO> waitingRecipeSOs;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount = 0;

    [field: SerializeField] public DeliveryCounter[] DeliveryCounters { get; private set; }
    [field: SerializeField] public DeliveryResultUI[] DeliveryResultUIs { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        waitingRecipeSOs = new List<RecipeSO>();
    }

    private void Update()
    {
        if (!IsServer)
        {
            return;
        }
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOs.Count < waitingRecipesMax)
            {
                int waitingRecipeSOIndex = UnityEngine.Random.Range(0, recipeSOList.RecipeSOs.Count);
                
                
                SpawnNewWaitingRecipeClientRpc(waitingRecipeSOIndex);
            }
            
        }
    }

    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeSOIndex)
    {
        RecipeSO waitingRecipeSO = recipeSOList.RecipeSOs[waitingRecipeSOIndex];
        waitingRecipeSOs.Add(waitingRecipeSO);

        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject, int index)
    {
        for (int i = 0; i < waitingRecipeSOs.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOs[i];

            if (waitingRecipeSO.KitchenObjectSOs.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;

                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.KitchenObjectSOs)
                {
                    bool ingredientFound = false;

                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    DeliverCorrectRecipeServerRpc(i, index);
                    return;
                }
            }
        }

        DeliverIncorrectRecipeServerRpc(index);
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverIncorrectRecipeServerRpc(int index)
    {
        DeliverIncorrectRecipeClientRpc(index);
    }

    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc(int index)
    {
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);


        DeliveryResultUIs[index].OnRecipeFaild();

    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int waitingRecipeSOListIndex, int index)
    {
        DeliverCorrectRecipeClientRpc(waitingRecipeSOListIndex, index);
    }

    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int waitingRecipeSOListIndex, int index)
    {
        successfulRecipesAmount++;
        waitingRecipeSOs.RemoveAt(waitingRecipeSOListIndex);
        OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
        OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

        DeliveryCounters[index].AddSuccessfulRecipesCountClientRpc();
        DeliveryResultUIs[index].OnRecipeSuccess();
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOs;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
