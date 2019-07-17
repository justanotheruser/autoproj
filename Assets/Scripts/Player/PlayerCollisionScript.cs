using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionScript : MonoBehaviour
{
    public LevelManager LevelMgr { set { levelMgr = value; } }

    private LevelManager levelMgr;

    private void OnCollisionEnter(Collision collision)
    {
        levelMgr.OnPlayerCollision(collision);
    }
}
