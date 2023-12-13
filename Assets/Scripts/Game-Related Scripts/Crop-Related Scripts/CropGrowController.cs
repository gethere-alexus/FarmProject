using UnityEngine;

public class CropGrowController : MonoBehaviour
{
    [SerializeField] private Crop _typeOfCrop;
    
    private CultivatedDirt _onPlantedTile;
    
    private CropSpriteUpdater _cropSpriteUpdater;
    private CropToCollectController _cropToCollectController;

    private int _currentLifeStage = 0;
    private float _changeStageAfter;
    
    [SerializeField] 
    private float _timePastSincePlanted, _timePastSinceGrew, _timePastSinceIncreased;
    private bool _isCropReadyToCollect, _isCropDecaying, _isLifeCycleOver;
    
    private void Start()
    {
        _cropToCollectController = this.gameObject.GetComponent<CropToCollectController>();
        _cropSpriteUpdater = this.gameObject.GetComponent<CropSpriteUpdater>();
        
        _changeStageAfter = _typeOfCrop.ChangeGrowingStageAfter;
        
        UpdateCropStage();
    }
    
    private void Update()
    {
        _timePastSincePlanted += Time.deltaTime;
        _timePastSinceIncreased += Time.deltaTime;

        if (!_isLifeCycleOver && _timePastSinceIncreased >= _changeStageAfter)
        {
            IncreaseCropStage();
            _timePastSinceIncreased = 0;
        }
    }
    public void IncreaseCropStage()
    { 
        if (_currentLifeStage + 1 >= _typeOfCrop.GetAmountOfLifeStages())
        {
            _isLifeCycleOver = true;
        }
        else
        {
            _currentLifeStage++;
            UpdateCropStage();   
        }
    }
    private void UpdateCropStage()
    {
        bool isCropReady = (_currentLifeStage == _typeOfCrop.AmountOfGrowingStages);
        bool isCropDecaying = (_currentLifeStage >= _typeOfCrop.AmountOfGrowingStages);
        
        if (isCropReady)
        {
            _cropToCollectController.SetBushReadiness(true);
            _changeStageAfter = _typeOfCrop.ChangeDecayingStageAfter;
        }
        else if (isCropDecaying)
        {
            _isCropDecaying = true;
        }
        
        _cropToCollectController.GiveStageCrop();
        _cropSpriteUpdater.SetSprite(_currentLifeStage);
    }
    
    public bool CheckIfDecaying
    {
        get => _isCropDecaying;
    }

    public float GetTimePastSincePlanted
    {
        get => _timePastSincePlanted;
    }
}
