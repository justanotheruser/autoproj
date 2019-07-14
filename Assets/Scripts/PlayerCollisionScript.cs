using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionScript : MonoBehaviour
{
    public UnityEvent onDeathEvent;
    public UnityEvent onCollisionExitEvent; //TODO temp

    private void OnCollisionEnter(Collision collision)
    {
        //TODO fix this asap
        Vector3 pos = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        pos.z = 0;
        transform.position = pos; 

        onDeathEvent.Invoke();
    }

    private void OnCollisionExit(Collision collision)
    {
        onCollisionExitEvent.Invoke();
    }


}
