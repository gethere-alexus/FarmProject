using UnityEngine;

public class CropToCollectController : MonoBehaviour
{
    [SerializeField] 
    private Crop _typeOfCrop;
    private CultivatedDirt _onPlantedTile;
    private CropGrowController _cropGrowController;

    private bool _isBushReadyToCrop;
    
    [SerializeField] 
    private int _cropToCollect;
    private int _cropToTakePerDecayingStage;

    void OnEnable()
    {
        _onPlantedTile = this.gameObject.GetComponentInParent<CultivatedDirt>();
        _cropGrowController = this.gameObject.GetComponent<CropGrowController>();
    }

    public void SetBushReadiness(bool isReady)
    {
        _cropToTakePerDecayingStage = -(_cropToCollect / 3);
        _isBushReadyToCrop = isReady;
        _onPlantedTile.SetTileBusyness(true);
    }

    public void GiveStageCrop()
    {
        
        int cropToGive = _cropGrowController.CheckIfDecaying ? _cropToTakePerDecayingStage : GetGrowingStageCrop();
        _cropToCollect += cropToGive;
        if (_cropToCollect < 0)
        {
            _cropToCollect = 0;
        }
    }
    private int GetGrowingStageCrop()
    {
        return Random.Range(_typeOfCrop.MinAmountOfStageCrop, _typeOfCrop.MaxAmountOfStageCrop) * _onPlantedTile.GetDirtQuality();
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
