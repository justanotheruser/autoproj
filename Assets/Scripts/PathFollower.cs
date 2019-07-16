using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private List<DragController.Node> nodes = new List<DragController.Node>();

    private int currentNode;
    private float currentDelay;
    private Vector3 currentTarget;
    private Vector3 currentStart;

    private float timer;


    public bool TouchOver { get; private set; }

    private void Start()
    {
        currentStart = transform.position;
    }

    private void Update()
    {
        if (nodes.Count > 0)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Slerp(currentStart, currentTarget, timer / currentDelay);

            if (timer > currentDelay)
            {
                timer -= currentDelay;
                currentNode++;
                if (currentNode > nodes.Count - 1)
                    StopMotion();
                else
                {
                    currentStart = currentTarget;
                    currentDelay = nodes[currentNode].delay;
                    currentTarget = nodes[currentNode].position;
                }
            }
        }
    }

    public void SetNodes(List<DragController.Node> nodes)
    {
        currentNode = 0;
        this.nodes = new List<DragController.Node>(nodes);
    }

    public void StopMotion()
    {
        nodes.Clear();
    }


    private void OnMouseEnter()
    {
        TouchOver = true;
    }
    private void OnMouseExit()
    {
        TouchOver = false;
    }
}
