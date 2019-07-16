using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionScript : MonoBehaviour
{
    public UnityEvent onDeathEvent;

    private void OnCollisionEnter(Collision collision)
    {
        //TODO fix asap
        Vector3 pos = GameObject.FindGameObjectWithTag("Respawn").transform.position; //KILL
        pos.z = 0;  //ME
        transform.position = pos; //PLS

        onDeathEvent.Invoke();
    }


}
