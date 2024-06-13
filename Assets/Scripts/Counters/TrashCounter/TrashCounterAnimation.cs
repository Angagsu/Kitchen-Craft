using UnityEngine;

public class TrashCounterAnimation : MonoBehaviour
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
            animator.SetTrigger(leftSideOpenHash);
        }
        else
        {
            animator.SetTrigger(rightSideOpenHash);
        }
    }
}
