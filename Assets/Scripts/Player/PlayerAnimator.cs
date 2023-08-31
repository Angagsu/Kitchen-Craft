using UnityEngine;
using Unity.Netcode;


public class PlayerAnimator : NetworkBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Player player;

    private int isWalking;

    private void Start()
    {
        isWalking = Animator.StringToHash("IsWalking");
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        animator.SetBool(isWalking, player.IsWalking());
    }
    
}
