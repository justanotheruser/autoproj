using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDragPath : MonoBehaviour
{
    public struct TouchDragPathNode
    {
        public readonly float delay;
        public readonly Vector3 position;

        public TouchDragPathNode(float delay, Vector3 position)
        {
            this.delay = delay;
            this.position = position;
        }
    }

    public PathFollower player;
    private Camera cam;

    public bool debug = true;
    private static readonly Vector3 debugNodeScale = new Vector3(0.2f, 0.2f, 0.2f);


    private bool dragging = false;
    private float timer;
    [SerializeField] private float distanceInterval = 0.1f;

    public List<TouchDragPathNode> Nodes { get; private set; } = new List<TouchDragPathNode>();
    private Transform lastNode;

    private void Start()
    {
        cam = Camera.main;
        lastNode = player.transform;
    }

    //TODO temp
    private void OnValidate()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().enabled = debug; // disable/enable nodes' renderer
        }

    }


    void Update()
    {
        timer += Time.deltaTime;

        Vector3 touchPos = Vector3.zero;

        //touch input
        if (Application.platform == RuntimePlatform.Android && Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) ResetNodes(transform, touch.position);
            if (touch.phase == TouchPhase.Ended) FinishPath();
            if (touch.phase == TouchPhase.Moved) touchPos = touch.position;
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) ResetNodes(transform, Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) FinishPath();
            if (Input.GetMouseButton(0)) touchPos = Input.mousePosition;
        }

        //input processing
        if (dragging && touchPos != Vector3.zero)
        {
            touchPos.z = -cam.transform.position.z;
            Vector3 touchWorld = cam.ScreenToWorldPoint(touchPos);
            //float dst = Vector3.Distance(lastNode.position, touchWorld); //TODO use magnitude shit for opti
            float dst = (lastNode.position - touchWorld).sqrMagnitude;
            if (dst > (distanceInterval * distanceInterval)) //checks distance so nodes wont generate too close to each other
            {
                CreateNode(touchWorld);
                timer = 0;
            }
        }

    }

    private void FinishPath()
    {
        if (dragging)
        {
            player.SetNodes(Nodes);
            dragging = false;
        }
    }

    private void CreateNode(Vector3 v)
    {
        Nodes.Add(new TouchDragPathNode(timer, v));
        lastNode = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        lastNode.parent = transform;
        lastNode.localScale = debugNodeScale;
        lastNode.transform.position = v;

        lastNode.gameObject.GetComponent<Renderer>().enabled = debug; //TODO temp
    }

    private void ResetNodes(Transform nodesHolder, Vector3 touchPoint)
    {
        dragging = IsTouchOverPlayer(touchPoint);
        Debug.Log("Touch started " + (dragging ? "over" : "OFF") + " player position");
        if (dragging)
        {
            timer = 0;
            Nodes.Clear();
            lastNode = player.transform;
            foreach (Transform child in nodesHolder)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private bool IsTouchOverPlayer(Vector3 touchPoint)
    {
        //TODO fix asap
        return true;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        return Physics.Raycast(ray, out hit, player.gameObject.layer);

    }
}
