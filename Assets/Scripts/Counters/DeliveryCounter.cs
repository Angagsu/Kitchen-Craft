using Unity.Netcode;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    [SerializeField] private int index = 0;

    private int successfulRecipesAmount;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject, index);

                KitchenObject.DestroyKitchenObject(player.GetKitchenObject());
            }
        }
    }

    [ClientRpc]
    public void AddSuccessfulRecipesCountClientRpc()
    {
        successfulRecipesAmount++;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
