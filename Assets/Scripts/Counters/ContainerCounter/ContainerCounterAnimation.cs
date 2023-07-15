using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimation : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;
    private  const string OPEN_CLOSE = "OpenClose";

    private void Start()
    {
        animator = GetComponent<Animator>();
        containerCounter.OnPlayerGrabbedObject += OnPlayerGrabbedObject;
    }

    private void OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
