using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CultivatedDirt : Tile, IPlantable
{
    private string _pathToCropPrefab;
    private bool _isTilePlanted, _isTileReadyToCrop;
    private int _qualityOfCultivatedDirt;
    private int _amountOfCrop;

    private void OnEnable()
    {
        _pathToCropPrefab = "Prefabs/Crops/Raspberry";
        _qualityOfCultivatedDirt = SetRandomQualityValue();
            
        this.gameObject.transform.rotation = quaternion.identity;
    }

    public void Plant()
    {
        if (!_isTilePlanted)
        {
            _isTilePlanted = true;
            
            GameObject bush = Instantiate(Resources.Load<GameObject>(_pathToCropPrefab), this.gameObject.transform);
            bush.transform.position = this.gameObject.transform.position;
            
            GlobalEventBus.Sync.Publish(this, new OnTilePlanted(this.gameObject));
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
        CropToCollectController bushCropCollector = GetComponentInChildren<CropToCollectController>();
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
}
