using System;
using UnityEngine;

enum ObjectTypes {BorderTile,SandTile,GrassTile,DirtTile,CultivatedDirtTile, Player, Crop}
public abstract class SaveableObjects : MonoBehaviour
{
    protected string Save;

    [SerializeField]
    private ObjectTypes _objectType;

    private void Start()
    {
        GameSaver.Instance.objectsToSave.Add(this);
    }

    private void OnDestroy()
    {
        GameSaver.Instance.objectsToSave.Remove(this);
    }

    public void SaveObject(int id)
    {
        PlayerPrefs.SetString(id.ToString(), _objectType + "_" + transform.position.ToString());
    }

    public virtual void LoadObject(string[] values)
    {
        transform.localPosition = GameSaver.Instance.StringToVector(values[1]);
    }

    public virtual void DestroyObject()
    {
        
    }
}
