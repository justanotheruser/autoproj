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

    private float timer;
    [SerializeField] private float timeInterval = 0.01f;
    [SerializeField] private float distanceInterval = 0.1f;

    public List<TouchDragPathNode> Nodes { get; private set; } = new List<TouchDragPathNode>();
    private Transform lastNode;

    private void Start()
    {
        cam = Camera.main;
        lastNode = transform;
    }

    //TODO temp
    private void OnValidate() //for checking debug bool in editor
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
                    touchPos = touch.position;
                }
            }
            else
            {
                if (Input.GetMouseButton(0)) touchPos = Input.mousePosition;
                if (Input.GetMouseButtonDown(0)) ResetNodes(transform);
                if (Input.GetMouseButtonUp(0)) player.SetNodes(Nodes);
            }

            if (touchPos != Vector3.zero)
            {
                touchPos.z = -cam.transform.position.z;
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


    private void CreateNode(Vector3 v)
    {
        Nodes.Add(new TouchDragPathNode(timer, v));
        lastNode = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        lastNode.parent = transform;
        lastNode.localScale = debugNodeScale;
        lastNode.transform.position = v;

        lastNode.gameObject.GetComponent<Renderer>().enabled = debug; //TODO temp
    }

    private void ResetNodes(Transform t)
    {
        timer = 0;
        Nodes.Clear();
        lastNode = transform;
        foreach (Transform child in t)
        {
            Destroy(child.gameObject);
        }
    }
}
