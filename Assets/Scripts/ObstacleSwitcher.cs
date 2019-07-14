using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSwitcher : MonoBehaviour
{
    public GameObject obstacleLeft;
    public GameObject obstacleRight;

    [SerializeField] private float switchDelay = 1f;

    private float timer;
    private bool rightActive;

    private void Start()
    {
        Switch();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > switchDelay)
        {
            timer -= switchDelay;
            Switch();
        }
    }

    private void Switch()
    {
        rightActive = !rightActive;
        obstacleLeft.SetActive(!rightActive);
        obstacleRight.SetActive(rightActive);
    }
}
