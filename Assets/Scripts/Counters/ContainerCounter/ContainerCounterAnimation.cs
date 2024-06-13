using UnityEngine;

public class ContainerCounterAnimation : MonoBehaviour
{
    private Animator animator;

    private int leftSideOpenHash;
    private int rightSideOpenHash;


    private void Start()
    {
        animator = GetComponent<Animator>();
        leftSideOpenHash = Animator.StringToHash("LeftSideOpen");
        rightSideOpenHash = Animator.StringToHash("RightSideOpen");
    }

    public void SetAnimationTrigger(Player player)
    {
        if (player.PlayerTeam == 0)
        {
            animator.SetTrigger(rightSideOpenHash);
        }
        else
        {
            animator.SetTrigger(leftSideOpenHash);
        }
    }
}
