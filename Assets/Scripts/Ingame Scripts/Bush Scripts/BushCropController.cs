using UnityEngine;

public class BushCropController : MonoBehaviour
{
    private CultivatedDirt _onPlantedTile;
    private BushGrowController _bushGrowController;

    private bool _isBushReadyToCrop;
    
    private int _cropToCollect;
    private int _cropToTakePerDecayingStage;
    private int _minimumAmountOfCropPerStage = 125, _maximumAmountOfCropPerStage = 250;

    void OnEnable()
    {
        _onPlantedTile = this.gameObject.GetComponentInParent<CultivatedDirt>();
        _bushGrowController = this.gameObject.GetComponent<BushGrowController>();
    }

    public void SetBushReadiness(bool isReady)
    {
        _cropToTakePerDecayingStage = -(_cropToCollect / 3);
        _isBushReadyToCrop = isReady;
        _onPlantedTile.SetTileBusyness(true);
    }

    public void GiveStageCrop()
    {
        
        int cropToGive = _bushGrowController.CheckIfDecaying ? _cropToTakePerDecayingStage : GetGrowingStageCrop();
        _cropToCollect += cropToGive;
        if (_cropToCollect < 0)
        {
            _cropToCollect = 0;
        }
    }
    private int GetGrowingStageCrop()
    {
        return Random.Range(_minimumAmountOfCropPerStage, _maximumAmountOfCropPerStage) * _onPlantedTile.GetDirtQuality();
    }

    public void DecreaseAmountOfCrop(int amountToReduce)
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
