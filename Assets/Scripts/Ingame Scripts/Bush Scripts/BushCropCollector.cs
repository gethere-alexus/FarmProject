using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushCropCollector : MonoBehaviour
{
    private CultivatedDirt _onPlantedTile;

    private bool _isBushReadyToCrop;
    [SerializeField]private int _cropToCollect, _minAmountOfCrop = 500, _maxAmountOfCrop = 1000;

    void Start()
    {
        _onPlantedTile = this.gameObject.GetComponentInParent<CultivatedDirt>();
        _cropToCollect = Random.Range(_minAmountOfCrop, _maxAmountOfCrop);
        _cropToCollect *= _onPlantedTile.GetDirtQuality();
    }

    public void SetBushReadiness(bool isReady)
    {
        _isBushReadyToCrop = isReady;
        _onPlantedTile.SetTileBusyness(true);
    }

    public void ReduceAmountOfCrop(int amountToReduce)
    {
        _cropToCollect -= amountToReduce;
    }

    public void IncreaseAmountOfCrop(int amountToGive)
    {
        _cropToCollect += amountToGive;
    }

    public int GetAmountOfCrop()
    {
        return _isBushReadyToCrop ? _cropToCollect : 0;
    }
}
