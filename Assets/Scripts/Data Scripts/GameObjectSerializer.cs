
using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class GameObjectSerializer : MonoBehaviour
{
    private string _saveFilePath;
    [SerializeField] private GameObject _map;

    private void Start()
    {
        _saveFilePath = Application.persistentDataPath;
    }

    public void SaveGameObjectAsJson()
    {
        
    }

    public GameObject LoadJSON()
    {
        return new GameObject();
    }
}
