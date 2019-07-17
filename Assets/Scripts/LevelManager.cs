using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sets up and manages single level
public class LevelManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] checkpoints;

    private GameObject currentCheckpoint;
    private PlayerCollisionScript _playerCollisions;
    private PathFollower _pathFollower;
    private PlayerSeletor _playerSelector;

    void Awake()
    {
        _playerCollisions = player.GetComponent<PlayerCollisionScript>();
        if (_playerCollisions == null)
            Debug.LogError("Player object is broken");
        _playerCollisions.LevelMgr = this;

        _pathFollower = player.GetComponent<PathFollower>();
        if (_pathFollower == null)
            Debug.LogError("Player object is broken");

        _playerSelector = player.GetComponent<PlayerSeletor>();
        if (_playerSelector == null)
            Debug.LogError("Player object is broken");

        if (checkpoints == null || checkpoints.Length == 0)
            Debug.LogError("No checkpoint for level are set");
        currentCheckpoint = checkpoints[0];

        DragController.instance.LevelMgr = this;
    }

    void Start()
    {
        MovePlayerToCheckpoint();
    }

    public void OnPlayerCollision(Collision collision)
    {
        Debug.Log(string.Format("Player collided with {0}", collision.collider));
        // TODO: if collider is obstacle
            _pathFollower.StopMotion();
            RevertToCheckpoint();
    }

    private void MovePlayerToCheckpoint()
    {
        Debug.Log(string.Format("sets player to {0}", currentCheckpoint.transform.position));
        player.transform.position = currentCheckpoint.transform.position;
        DragController.instance.StartPosition = player.transform.position;
    }

    void RevertToCheckpoint()
    {
        MovePlayerToCheckpoint();
        // TODO: this function should 
        // 1) reset all events that occur on timer
        // 2) move all movable objects to the state they were in when player reached current checkpoint
    }

    public void SetNodes(List<DragController.Node> nodes)
    {
        _pathFollower.SetNodes(nodes);
    }

    public bool IsTouchOverPlayer(Vector3 touchPoint)
    {
        return _playerSelector.TouchOver;
    }
}
