
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    private static GameSaver _instance;
    public List<SaveableObjects> objectsToSave { get; private set; }
    public static GameSaver Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameSaver>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        objectsToSave = new List<SaveableObjects>();
    }

    public void Save()
    {
        
        PlayerPrefs.SetInt("ObjectCount", objectsToSave.Count);
        
        for (int id = 0; id < objectsToSave.Count; id++)
        {
            objectsToSave[id].SaveObject(id);
        }
        Debug.Log("SAVED");
    }

    public void Load()
    {
        foreach (SaveableObjects obj in objectsToSave)
        {
            if (obj != null)
            {
                Destroy(obj.gameObject);
            }
        }
        
        objectsToSave.Clear();
        
        int objectCount = PlayerPrefs.GetInt("ObjectCount");

        for (int i = 0; i < objectCount; i++)
        {
            string[] value = PlayerPrefs.GetString(i.ToString()).Split('_');
            GameObject temp = Instantiate(Resources.Load("Prefabs/Tiles/" + value[0]) as GameObject);
            temp.GetComponent<SaveableObjects>().LoadObject(value);
            Debug.Log(value);
        }
    }

    public Vector3 StringToVector(string stringToConvert)
    {
        stringToConvert = stringToConvert.Trim(new char[] { '(', ')' }).Replace(" ", "");
        string[] coordinates = stringToConvert.Split(',');
        
        return new Vector3(float.Parse(coordinates[0]), float.Parse(coordinates[1]), float.Parse(coordinates[2]));
    }

    public Quaternion StringToQuaternion(string stringToConvert)
    {
        return Quaternion.identity;
    }
}
