using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO Input;
    public KitchenObjectSO Output;
    public float BurningTimerMax;
}
