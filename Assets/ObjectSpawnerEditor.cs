using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ObjectSpawnerScript))]
public class ObjectSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        Color color;

        ObjectSpawnerScript obj = (ObjectSpawnerScript)target;

        GUILayout.Space(20);
        if (GUILayout.Button("Create new level"))
        {
            obj.CreateHolder();
        }
        GUILayout.Space(20);

        if (obj.objects != null)
            for (int i = 0; i < obj.objects.Length; i++)
            {
                GUIStyle style = GUI.skin.button;
                GUI.backgroundColor = Color.black;
                style.richText = true;

                color = Color.HSVToRGB((float)i / obj.objects.Length, 1f, 1f);

                if (GUILayout.Button("<color=#" +
                    ColorUtility.ToHtmlStringRGB(color) +
                    ">" + obj.objects[i].name + "</color>", style))
                {
                    obj.SpawnObject(i);
                }
            }

    }
}
