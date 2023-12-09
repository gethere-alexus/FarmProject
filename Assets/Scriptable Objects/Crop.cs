using UnityEngine;
using UnityEngine.SceneManagement;
public enum CropLifeStages
{
    FirstGrowingStage,SecondGrowingStage,ThirdGrowingStage,LastGrowingStage,
    FirstDecayingStage,SecondDecayingStage,ThirdDecayingStage,LastDecayingStage,
}
public interface IDifficultyDepended
{
    public void AdjustDifficultyDependedProperties(Scene scene, LoadSceneMode loadSceneMode);
}
[CreateAssetMenu(fileName = "Crop")]
public class Crop : ScriptableObject, IDifficultyDepended
{
    private float _levelDifficulty;
    
    [SerializeField] private float _timeNeedToGrow = 15.0f;
    [SerializeField] private float _timeNeedToDecay = 120.0f;
    
    [SerializeField] private int _minAmountOfStageCrop = 1250;
    [SerializeField] private int _maxAmountOfStageCrop = 2500;
    
    [SerializeField] private Sprite[] _bushGrowingStagesSprites;
    [SerializeField] private Sprite[] _bushDecayingStagesSprites;

    [SerializeField]private float _modifiedTimeNeedToGrow;
    [SerializeField]private float _modifiedTimeNeedToDecay;

    [SerializeField]private int _modifiedMinAmountOfStageCrop;
    [SerializeField]private int _modifiedMaxAmountOfStageCrop;
    
    [SerializeField]private float _changeGrowingStageAfter;
    [SerializeField]private float _changeDecayingStageAfter;
    
    private int _amountOfGrowingStages;
    private int _amountOfDecayingStages;

    public void AdjustDifficultyDependedProperties(Scene scene, LoadSceneMode loadSceneMode)
    {
        int gameSceneBuildIndex = 2;
        if (scene.buildIndex == gameSceneBuildIndex)
        {
            int difficulty = (int)PlayerPrefs.GetFloat(PropertyTypes.Difficulty.ToString());

            _modifiedTimeNeedToGrow *= difficulty;
            _modifiedTimeNeedToDecay /= difficulty;
            _modifiedMinAmountOfStageCrop /= difficulty;
            _modifiedMaxAmountOfStageCrop /= difficulty;
            
            _changeGrowingStageAfter = _modifiedTimeNeedToGrow / _amountOfGrowingStages;
            _changeDecayingStageAfter = _modifiedTimeNeedToDecay / _amountOfDecayingStages;
        }
    }
    private void OnEnable()
    {
        _modifiedTimeNeedToGrow = _timeNeedToGrow;
        _modifiedTimeNeedToDecay = _timeNeedToDecay;
        _modifiedMinAmountOfStageCrop = _minAmountOfStageCrop;
        _modifiedMaxAmountOfStageCrop = _maxAmountOfStageCrop;
        
        _amountOfGrowingStages = _bushGrowingStagesSprites.Length;
        _amountOfDecayingStages = _bushDecayingStagesSprites.Length;
        
        SceneManager.sceneLoaded += AdjustDifficultyDependedProperties;
    }

    public int MinAmountOfStageCrop
    {
        get => _modifiedMinAmountOfStageCrop;
    }
    public int MaxAmountOfStageCrop
    {
        get => _modifiedMaxAmountOfStageCrop;
    }
    public Sprite GetBushGrowingStageSprite(int stage)
    {
        if (stage >= _amountOfGrowingStages)
        {
            return _bushDecayingStagesSprites[stage - _amountOfGrowingStages];
        }
        else
        {
            return _bushGrowingStagesSprites[stage];
        }
    }

    public int GetAmountOfLifeStages()
    {
        return _amountOfDecayingStages + _amountOfGrowingStages;
    }

    public int GetDecayingStartingStage()
    {
        return _amountOfGrowingStages;
    }
    public int AmountOfGrowingStages
    {
        get => _amountOfGrowingStages - 1;
    }

    public float ChangeGrowingStageAfter
    {
        get => _changeGrowingStageAfter;
    }
    public float ChangeDecayingStageAfter
    {
        get => _changeDecayingStageAfter;
    }
}
