using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DragController : MonoBehaviour
{
    public static DragController instance;

    public struct Node
    {
        public readonly float delay;
        public readonly Vector3 position;

        public Node(float delay, Vector3 position)
        {
            this.delay = delay;
            this.position = position;
        }
    }
    public LevelManager LevelMgr { set { levelMgr = value; } }
    public Vector3 StartPosition { set { startPos = value; } }

    private Vector3 startPos;
    private LevelManager levelMgr;
    private LineRenderer lineRenderer;
    private Camera cam;

    public List<Node> Nodes { get; private set; } = new List<Node>();

    [SerializeField] private readonly float distanceInterval = 0.1f;
    private bool dragging = false;
    private float timer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (levelMgr == null)
            return;

        timer += Time.deltaTime;

        Vector3? touchPoint = GetTouchPoint();
        if (touchPoint.HasValue)
        {
            Vector3? nodePosition = GetNodePoint(touchPoint.Value);
            if (nodePosition != null)
                CreateNode(nodePosition.Value);
        }
    }

    private Vector3? GetTouchPoint()
    {
        if (Application.platform == RuntimePlatform.Android && Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) TouchStarted(touch.position);
            if (touch.phase == TouchPhase.Ended) TouchFinished();
            if (touch.phase == TouchPhase.Moved) return touch.position;
        }
        else
        {
            if (Input.GetMouseButtonDown(0)) TouchStarted(Input.mousePosition);
            if (Input.GetMouseButtonUp(0)) TouchFinished();
            if (Input.GetMouseButton(0)) return Input.mousePosition;
        }
        return null;
    }

    private void TouchStarted(Vector3 touchPoint)
    {
        dragging = IsTouchOverPlayer(touchPoint);
        Debug.Log("Touch started " + (dragging ? "<color=green>over</color>" : "<color=red>OFF</color>") + " player position");

        if (dragging)
            ResetNodes();
    }

    private bool IsTouchOverPlayer(Vector3 touchPoint)
    {
        return levelMgr.IsTouchOverPlayer(touchPoint);
        //TODO replace for raycast?
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //return Physics.Raycast(ray, out hit, player.gameObject.layer);
    }

    private void ResetNodes()
    {
        timer = 0;
        Nodes.Clear();

        lineRenderer.positionCount = 0;
        CreateNode(startPos);
    }

    private void TouchFinished()
    {
        Debug.Log("Touch finished");
        if (dragging)
            levelMgr.SetNodes(Nodes);
        dragging = false;
    }

    private Vector3? GetNodePoint(Vector3 touchPos)
    {
        if (dragging)
        {
            touchPos.z = -cam.transform.position.z;
            Vector3 touchWorld = cam.ScreenToWorldPoint(touchPos);
            float dst = (Nodes[Nodes.Count - 1].position - touchWorld).sqrMagnitude;

            if (dst > (distanceInterval * distanceInterval)) //checks distance so nodes wont generate too close to each other
                return touchWorld;

        }
        return null;
    }

    private void CreateNode(Vector3 v)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, v - (Vector3.forward * 0.1f)); //draw line a bit closer to the cam
        Nodes.Add(new Node(timer, v));
        timer = 0;
    }

}
