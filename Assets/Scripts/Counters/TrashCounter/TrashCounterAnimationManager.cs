using Unity.Netcode;
using UnityEngine;

public class TrashCounterAnimationManager : NetworkBehaviour
{
    public static TrashCounterAnimationManager Instance { get; private set; }

    [field: SerializeField] public TrashCounterAnimation[] TrashCountersAnimations { get; private set; }

    private Player currentPlayer;

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
    }

    [ClientRpc]
    public void SetAnimationForCurrentContainerClientRpc(int index)
    {
        TrashCountersAnimations[index].SetAnimationTrigger(currentPlayer);
    }

    [ClientRpc]
    public void GetPlayerClientRpc(int index)
    {
        currentPlayer = GameManager.Instance.GetPlayerFromConnectedPlayersIndex(index);
    }
}
