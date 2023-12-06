using System;
using UnityEngine;

[Serializable]
public class GameObjectData 
{
    public GameObject Object;

    public GameObjectData(GameObject gameObject)
    {
        Object = gameObject;
    }

   
}
