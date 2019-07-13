using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    private List<TouchDragPath.TouchDragPathNode> nodes;

    private bool moving = false;

    private int currentNode;
    private float currentDelay;
    private Vector3 currentTarget;
    private Vector3 startPosition;

    private float timer;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (moving)
        {
            timer += Time.deltaTime;
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
                    startPosition = currentTarget;
                    currentDelay = nodes[currentNode].delay;
                    currentTarget = nodes[currentNode].position;
                }
            }

            transform.position = Vector3.Lerp(startPosition, currentTarget, timer / currentDelay);
        }
    }

    public void SetNodes(List<TouchDragPath.TouchDragPathNode> nodes)
    {
        this.nodes = nodes;
        moving = true;
        currentNode = 0;
    }
}
