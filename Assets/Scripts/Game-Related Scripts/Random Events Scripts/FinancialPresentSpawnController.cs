using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinancialPresentSpawnController : MonoBehaviour, IDifficultyDepended, IPauseable
{
   [SerializeField] private GameObject _presentPrefab;
   
   [SerializeField, Tooltip("In Seconds")]private float _minimumTimeDelay = 60.0f;
   [SerializeField, Tooltip("In Seconds")]private float _maximumTimeDelay = 120.0f;

   [SerializeField] private float _timePastWithoutPresent;
   [SerializeField] private float _timeCoolDown;
   
   private float _modifiedMinimumTimeDelay;
   private float _modifiedMaximumTimeDelay;
   
   private bool _doesPresentExist;
   private bool _isScriptPaused;
   public void AdjustDifficultyDependedProperties()
   {
      int difficulty = (int)PlayerPrefs.GetFloat(PropertyTypes.Difficulty.ToString());

      _modifiedMinimumTimeDelay = _minimumTimeDelay * difficulty;
      _modifiedMaximumTimeDelay = _maximumTimeDelay * difficulty;
   }
   private void CreateFinancialPresent()
   {
      Instantiate(_presentPrefab);
      GlobalEventBus.Sync.Publish(this, new OnFinancialPresentAppeared());
      
      _timePastWithoutPresent = 0;
      
      _doesPresentExist = true;
   }
   private void GenerateNewPresentValues()
   {
      _timeCoolDown = Random.Range(_modifiedMinimumTimeDelay, _modifiedMaximumTimeDelay);
   }
   private void ProccessPauseSignal(object sender, EventArgs eventArgs)
   {
      OnGamePausePerformed onGamePausePerformed = (OnGamePausePerformed)eventArgs;
      
      SwitchPauseState(onGamePausePerformed.IsGamePaused);
   }

   private void ProccessPresentDeletedSignal(object sender, EventArgs eventArgs)
   {
      GenerateNewPresentValues();
      _doesPresentExist = false;
      _timePastWithoutPresent = 0;
   }
   public void SwitchPauseState(bool isPaused)
   {
      _isScriptPaused = isPaused;
   }
   private void Awake()
   {
      AdjustDifficultyDependedProperties();
   }

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnGamePausePerformed>(ProccessPauseSignal);
      GlobalEventBus.Sync.Subscribe<OnFinancialPresentDeleted>(ProccessPresentDeletedSignal);
   }
   private void Start()
   {
      GenerateNewPresentValues();
   }
   private void FixedUpdate()
   {
      if (!_isScriptPaused)
      {
         if (!_doesPresentExist)
         {
            _timePastWithoutPresent += Time.deltaTime;
      
            if (_timePastWithoutPresent >= _timeCoolDown)
            {
               CreateFinancialPresent();
            }
         }
      }
   }
   private void OnDisable()
   {
      GlobalEventBus.Sync.Unsubscribe<OnGamePausePerformed>(ProccessPauseSignal);
      GlobalEventBus.Sync.Unsubscribe<OnFinancialPresentDeleted>(ProccessPresentDeletedSignal);
   }
}
