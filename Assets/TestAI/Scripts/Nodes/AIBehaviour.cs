using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    private BehaviourTree behaviourTree;

    private void Start()
    {
        behaviourTree = new();

        Node steal = new Node(name: "Steal Everything");
        Node goToDiamond = new Node(name: "GoToDiamond");
        Node goToCar = new Node(name: "GoToCar");

        steal.AddChild(goToDiamond);
        steal.AddChild(goToCar);
        behaviourTree.AddChild(steal);

        Node eat = new Node("Eat Something");
        Node dumplings = new Node("Eat dumplings");
        Node shaverma = new Node("Eat Shaverma");
        
        eat.AddChild(dumplings);
        eat.AddChild(shaverma);
        behaviourTree.AddChild(eat);
        behaviourTree.PrintTree();
    }
}
