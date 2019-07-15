using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLine : MonoBehaviour
{
    public Transform obstacle;
    public Transform sprite;

    [SerializeField] private bool vertical = true;
    [SerializeField] private float speed = 1;
    [SerializeField] private float length;

    private float position;
    private bool movingRight = true;

    private void OnValidate()
    {
        SetSpriteSize();
    }

    private void SetSpriteSize()
    {
        Vector3 scale = sprite.transform.localScale;
        if (vertical) { scale.x = length; scale.y = transform.localScale.y / 4; }
        else { scale.y = length; scale.x = transform.localScale.x / 4; }
        sprite.transform.localScale = scale;
    }

    private void Start()
    {
        SetSpriteSize();
    }

    private void Update()
    {
        position += (movingRight ? speed : -speed) * Time.deltaTime;
        float offlimit = Mathf.Abs(position) - length / 2;
        if (offlimit > 0)
        {
            movingRight = !movingRight;
            position += movingRight ? offlimit : -offlimit;
        }

        Vector3 obstaclePosition = transform.position;
        if (vertical) obstaclePosition.x = position;
        else obstaclePosition.y = position;
        obstacle.position = obstaclePosition;
    }

}
