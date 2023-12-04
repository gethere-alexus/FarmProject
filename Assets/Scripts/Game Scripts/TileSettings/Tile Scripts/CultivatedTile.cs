using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CultivatedDirt : Tile, IPlantable
{
    private string _pathToBushPrefab;
    private bool _isTilePlanted, _isTileReadyToCrop;
    private int _qualityOfCultivatedDirt;
    private int _amountOfCrop;

    private void OnEnable()
    {
        _pathToBushPrefab = "Prefabs/Bushes/Bush";
        _qualityOfCultivatedDirt = SetRandomQualityValue();
            
        this.gameObject.transform.rotation = quaternion.identity;
    }

    public void Plant()
    {
        if (!_isTilePlanted)
        {
            _isTilePlanted = true;
            GlobalEventBus.Sync.Publish(this, new OnTilePlanted(this.gameObject));
            InstantiateBush();
        }
    }

    public void Crop()
    {
        if (_isTileReadyToCrop)
        {
            GlobalEventBus.Sync.Publish(this, new OnCropCollected(this.gameObject, GetBushCrop()));
        }
    }
    
    public void SetTileBusyness(bool isReady)
    {
        _isTileReadyToCrop = isReady;
    }

    public int GetDirtQuality()
    {
        return _qualityOfCultivatedDirt;
    }

    private int GetBushCrop()
    {
        BushCropController bushCropCollector = GetComponentInChildren<BushCropController>();
        return bushCropCollector.GetAmountOfCrop();
    }
    private int SetRandomQualityValue()
    {
        float randomValue = Random.value;
           
        if (randomValue <= 0.4f)
        {
            return 1;
        }
        else if(randomValue > 0.4f && randomValue <= 0.7f)
        {
            return 2;
        }
        else if (randomValue > 0.7f && randomValue <= 0.85f)
        {
            return 3;
        }
        else if (randomValue > 0.85f && randomValue <= 0.95f)
        {
            return 4;
        }
        else if (randomValue > 0.95f)
        {
            return 5;
        }

        return 0;
    }
    private void InstantiateBush()
    {
        Vector3 positionFromTile = new Vector3(0, .5f, 0);
        GameObject bush = Instantiate(Resources.Load<GameObject>(_pathToBushPrefab), this.gameObject.transform);
        bush.transform.position = this.gameObject.transform.position + positionFromTile;
    }
}
