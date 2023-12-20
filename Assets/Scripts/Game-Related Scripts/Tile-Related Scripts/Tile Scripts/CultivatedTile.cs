using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CultivatedDirt : Tile, IPlantable
{
    [SerializeField] private GameObject _cropToPlant;
    private bool _isTilePlanted, _isTileReadyToCrop;
    
    private int _qualityOfCultivatedDirt;
    private int _amountOfCrop;
    
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnNewCropChosen>(SwitchCropToPlant);
        GlobalEventBus.Sync.Publish(this, new OnCultivatedDirtAppeared());
        
        _qualityOfCultivatedDirt = SetRandomQualityValue();
        this.gameObject.transform.rotation = quaternion.identity;
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnNewCropChosen>(SwitchCropToPlant);
    }
    private void SwitchCropToPlant(object sender, EventArgs eventArgs)
    {
        OnNewCropChosen onNewCropChosen = (OnNewCropChosen)eventArgs;
        _cropToPlant = onNewCropChosen.ChosenCrop;
    }

    public void Plant()
    {
        if (!_isTilePlanted)
        {
            _isTilePlanted = true;
            
            GameObject bush = Instantiate(_cropToPlant, this.gameObject.transform);
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

    public void SetActiveCrop(GameObject crop)
    {
        _cropToPlant = crop;
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
