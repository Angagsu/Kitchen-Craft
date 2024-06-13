using UnityEngine;
using Unity.Netcode;

public class ContainerAnimationManager : NetworkBehaviour
{
    public static ContainerAnimationManager Instance { get; private set; }

    [field: SerializeField] public ContainerCounterAnimation[] ContainerCounterAnimations { get; private set; }

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
        ContainerCounterAnimations[index].SetAnimationTrigger(currentPlayer);
    }

    [ClientRpc]
    public void GetPlayerClientRpc(int index)
    {
        currentPlayer = GameManager.Instance.GetPlayerFromConnectedPlayersIndex(index);
    }  
}
