﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectSpawnerScript : MonoBehaviour
{
    public GameObject[] objects;

    [HideInInspector] public Transform levelHolder;


    [MenuItem("WTF/Lock Inspector &q")]
    public static void UnlockInspector()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        Debug.Log(ActiveEditorTracker.sharedTracker.isLocked ? "<color=red>Locked</color>" : "<color=green>Unlocked</color>");
    }

    public void CreateHolder()
    {
        GameObject go = new GameObject("Level");
        go.transform.parent = transform;
        levelHolder = go.transform;
        ActiveEditorTracker.sharedTracker.isLocked = true;
        Selection.activeGameObject = go;
    }
    public void SpawnObject(int i)
    {
        if (levelHolder == null)
        {
            CreateHolder();
        }

        if (i < objects.Length)
            Instantiate(objects[i], levelHolder);
    }

}
