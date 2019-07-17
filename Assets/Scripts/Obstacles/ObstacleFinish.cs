using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ObstacleFinish : MonoBehaviour
{
    [SerializeField] private LevelLoader levelManager;

    private void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelLoader>(); //TODO how to avoid this?
    }

    private void OnCollisionEnter(Collision collision)
    {
        levelManager.Next();
    }
}
