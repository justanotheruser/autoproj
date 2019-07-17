using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeletor : MonoBehaviour
{
    public bool TouchOver { get; private set; }

    private void OnMouseEnter()
    {
        TouchOver = true;
    }
    private void OnMouseExit()
    {
        TouchOver = false;
    }
}
