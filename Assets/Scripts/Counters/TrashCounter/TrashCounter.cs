using UnityEngine;
using System;
using Unity.Netcode;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    [SerializeField] private int trashCounterIndex;

    private int playerIndex;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            playerIndex = GameManager.Instance.GetPlayerIndexFromConnectedPlayers(player);

            InteractLogicServerRpc(playerIndex);

            KitchenObject.DestroyKitchenObject(player.GetKitchenObject());
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void InteractLogicServerRpc(int index)
    {
        TrashCounterAnimationManager.Instance.GetPlayerClientRpc(index);
        TrashCounterAnimationManager.Instance.SetAnimationForCurrentContainerClientRpc(trashCounterIndex);
        InteractLogicClientRpc();
    }

    [ClientRpc]
    private void InteractLogicClientRpc()
    {
        OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
    }
}
