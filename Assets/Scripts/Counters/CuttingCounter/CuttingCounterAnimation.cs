using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimation : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;
    private const string Cut = "Cut";

    private void Start()
    {
        animator = GetComponent<Animator>();
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger(Cut);
    }
}
