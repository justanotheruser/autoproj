using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Checkpoint will be activated only if current checkpoint has same order or (same order - 1)
    public int order = 0;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("Collision with checkpoint {0}", this));
        if (other.GetComponents<PlayerCollisionScript>() != null)
        {
            Debug.Log(string.Format("Collision with checkpoint {0}", this));
        }
    }
}
