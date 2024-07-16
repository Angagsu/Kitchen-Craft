using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public enum Status
    {
        Success,
        Runing,
        Failure
    }

    public Status status;
    public List<Node> childrenNodes; 
    public int currentChildIndex;
    public string name;

    public Node()
    {

    }

    public Node(string name)
    {
        this.name = name;
    }

    public void AddChild(Node child)
    {
        childrenNodes.Add(child);
    }
}
