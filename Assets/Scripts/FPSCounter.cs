﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FPSCounter : MonoBehaviour
{
    public float updateRate = 1f;

    public int FPS { get; private set; }

    private void Start()
    {
        StartCoroutine(UpdateFPS());
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = Color.white;
        float msec = Time.deltaTime * 1000.0f;
        //float fps = 1.0f / Time.deltaTime;
        //string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        string text = FPS.ToString();
        GUI.Label(rect, text, style);
    }

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            FPS = (int)(1.0f / Time.deltaTime);
            yield return new WaitForSeconds(updateRate);
        }
    }

}
