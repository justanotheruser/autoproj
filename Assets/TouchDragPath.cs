using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDragPath : MonoBehaviour
{
    public FollowPath player;
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

    private Camera cam;

    private float timer;
    [SerializeField] private float timeInterval = 0.01f;
    [SerializeField] private float distanceInterval = 0.1f;

    private List<TouchDragPathNode> nodes = new List<TouchDragPathNode>();
    private Transform lastNode;

    public bool debug = true;
    private static readonly Vector3 debugNodeScale = new Vector3(0.1f, 0.1f, 0.1f);


    private void Start()
    {
        cam = Camera.main;
        lastNode = transform;
    }


    //TODO remove later
    private void OnValidate()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().enabled = debug;
        }

    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer > timeInterval) //checks time and distance intervals so nodes wont generate too close to each other
        {
            Vector3 touchPos = Vector3.zero;
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touches.Length > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began) ResetNodes(transform);
                    touchPos = Input.touches[0].position;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) ResetNodes(transform); //remove old path nodes on new mouse click
                if (Input.GetMouseButtonUp(0)) player.SetNodes(nodes);
                if (Input.GetMouseButton(0)) touchPos = Input.mousePosition;
            }

            if (touchPos != Vector3.zero)
            {
                touchPos.z = 10;
                Vector3 touchWorld = cam.ScreenToWorldPoint(touchPos);
                float dst = Vector3.Distance(lastNode.position, touchWorld); //TODO use magnitude shit for opti
                if (dst > distanceInterval)
                {
                    CreateNode(touchWorld);
                    timer = 0;
                }
            }
        }
    }

    private void ResetNodes(Transform t)
    {
        timer = 0;
        nodes.Clear();
        foreach (Transform child in t)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateNode(Vector3 v)
    {
        nodes.Add(new TouchDragPathNode(timer, v));
        lastNode = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        lastNode.parent = transform;
        lastNode.localScale = debugNodeScale;
        lastNode.transform.position = v;

        lastNode.gameObject.GetComponent<Renderer>().enabled = debug; //TODO remove later
    }

    public List<TouchDragPathNode> GetNodes()
    {
        return nodes;
    }
}
