using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{

    private Renderer renderer; //TODO temp

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnCollisionStay(Collision collision)
    {
        renderer.material.SetColor("_BaseColor", Color.red);
    }

    private void OnCollisionExit(Collision collision)
    {
        renderer.material.SetColor("_BaseColor", Color.green);
    }


}
