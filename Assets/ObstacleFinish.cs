using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFinish : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>(); //TODO how to avoid this?
    }

    private void OnCollisionEnter(Collision collision)
    {
        levelManager.Next();
    }
}
