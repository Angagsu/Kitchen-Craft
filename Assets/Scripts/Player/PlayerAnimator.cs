using UnityEngine;

public class PlayerAnimator : MonoBehaviour
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
        animator.SetBool(isWalking, player.IsWalking());
    }

}
