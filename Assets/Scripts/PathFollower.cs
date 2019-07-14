using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    private List<TouchDragPath.TouchDragPathNode> nodes;

    private bool moving = false; //TODO private?

    private int currentNode;
    private float currentDelay;
    private Vector3 currentTarget;
    private Vector3 currentStart;

    private float timer;

    private void Start()
    {
        currentStart = transform.position;
    }

    void Update()
    {
        if (moving)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Slerp(currentStart, currentTarget, timer / currentDelay);

            if (timer > currentDelay)
            {
                timer -= currentDelay;
                currentNode++;
                if (currentNode > nodes.Count - 1)
                {
                    moving = false;
                }
                else
                {
                    currentStart = currentTarget;
                    currentDelay = nodes[currentNode].delay;
                    currentTarget = nodes[currentNode].position;
                }
            }


        }
    }

    public void SetNodes(List<TouchDragPath.TouchDragPathNode> nodes)
    {
        this.nodes = nodes;
        moving = true;
        currentNode = 0;
    }

    public void StopMotion()
    {
        moving = false;
        nodes.Clear();
        currentNode = 0;
    }
}
