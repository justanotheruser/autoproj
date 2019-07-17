using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script sets up and manages single level
public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] checkpoints;

    private GameObject currentCheckpoint;

    void Awake()
    {
        if (checkpoints == null || checkpoints.Length == 0)
            Debug.LogError("No checkpoint for level are set");

        currentCheckpoint = checkpoints[0];
    }

    void Start()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}
