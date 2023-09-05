using System;
using UnityEngine;

[Serializable]
public class GameOption
{
    [SerializeField]
    private int objectsCount;
    public int ObjectsCount => objectsCount;

    public GameOption(int objectsCount)
    {
        this.objectsCount = objectsCount;
    }
}
