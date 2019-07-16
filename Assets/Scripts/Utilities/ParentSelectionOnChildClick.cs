using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParentSelectionOnChildClick : EditorWindow
{
    public static bool force = false;

    [MenuItem("Window/ParentSelectionHierarchyMonitor")]
    static void CreateWindow()
    {
        ParentSelectionOnChildClick window = (ParentSelectionOnChildClick)GetWindow(typeof(ParentSelectionOnChildClick));
        window.Show();
    }

    [MenuItem("WTF/Force Parent Selection On Child Click %q")]
    public static void SwitchForce()
    {
        force = !force;
        Debug.Log("<color=" + (force ? "red" : "green") + ">" + (force ? "PARENT" : "default") + "</color> selection");
    }

    [MenuItem("WTF/Unlock Inspector &q")]
    public static void UnlockInspector()
    {
        ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
        Debug.Log(ActiveEditorTracker.sharedTracker.isLocked ? "Locked" : "Unlocked");
    }

    private void OnSelectionChange()
    {
        if (force)
        {
            if (Selection.transforms.Length > 0)
            {
                Selection.activeGameObject = Selection.transforms[0].parent.parent.gameObject;
                Debug.Log("Force selected parent");
            }
        }
    }




}
