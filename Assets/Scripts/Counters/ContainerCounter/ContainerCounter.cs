using Unity.Netcode;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private int containerIndex;

    private int playerIndex;


    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            playerIndex = GameManager.Instance.GetPlayerIndexFromConnectedPlayers(player);

            InteractLogicServerRpc(playerIndex);   

            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicServerRpc(int index)
    {
        ContainerAnimationManager.Instance.GetPlayerClientRpc(index);
        ContainerAnimationManager.Instance.SetAnimationForCurrentContainerClientRpc(containerIndex);
    }
}
