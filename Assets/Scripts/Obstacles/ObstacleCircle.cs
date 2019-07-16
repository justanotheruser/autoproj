using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ObstacleCircle : MonoBehaviour
{
    public Transform obstacle;
    public Transform sprite;

    [SerializeField] private bool clockwise = true;
    [SerializeField] private float speed = 2f; //in radians
    [SerializeField] private float radius = 1;

    private float angle;

    //TODO temp
    private void OnValidate()
    {
        SetSpriteSize();
    }

    private void SetSpriteSize()
    {
        sprite.transform.localScale = Vector3.one * radius;
    }

    private void Start()
    {
        SetSpriteSize();
    }

    private void Update()
    {
        angle += (clockwise ? -speed : speed) * Time.deltaTime;
        angle %= Mathf.PI * 2f;

        Vector3 obstaclePosition = new Vector3();
        obstaclePosition.x = Mathf.Cos(angle);
        obstaclePosition.y = Mathf.Sin(angle);
        obstaclePosition *= radius / 2f;
        obstaclePosition += transform.position;
        obstacle.position = obstaclePosition;
    }

}
