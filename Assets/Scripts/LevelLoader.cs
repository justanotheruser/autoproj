using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
   public GameObject[] levels;

    private int currentIndex = -1;
    private GameObject currentLevel;

    private void Start()
    {
        Next();
    }

    public void Next()
    {
        currentIndex++;
        currentIndex %= levels.Length;

        Destroy(currentLevel);
        currentLevel = Instantiate(levels[currentIndex], transform);
    }
}
