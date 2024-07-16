using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BehaviourTree : Node
{
    public BehaviourTree() 
    {
         name = "Tree";
    }

    public BehaviourTree(string name)
    {
        this.name = name;
    }

    public void PrintTree()
    {
        string treePrint = "";
        Stack<Node> nodeStack = new Stack<Node>();
        Node currentNode = this;
        nodeStack.Push(currentNode);

        while (nodeStack.Count != 0)
        {
            Node nextNode = nodeStack.Pop();
            treePrint += nextNode.name + "\n";

            for(int i = nextNode.childrenNodes.Count - 1; i >= 0; i--)
            {
                nodeStack.Push(nextNode.childrenNodes[i]);
            }
        }
        Debug.Log(treePrint);
    }
}
