using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinancialPresentSpawnController : MonoBehaviour, IDifficultyDepended
{
   [SerializeField] private GameObject _presentPrefab;
   
   [SerializeField]private int _minimumAmountOfMoneyToPresent = 100;
   [SerializeField]private int _maximumAmountOfMoneyToPresent = 100;
   
   [SerializeField, Tooltip("In Seconds")]private float _minimumTimeDelay = 60.0f;
   [SerializeField, Tooltip("In Seconds")]private float _maximumTimeDelay = 120.0f;
   
   [SerializeField] private float _timeToDisapear = 0;
   
   private GameObject _presentInstance;

   private bool _doesPresentExist = false;
   
   private int _amountOfMoneyToPresent;
   private int _modifiedMinimumAmountOfMoneyToPresent = 100;
   private int _modifiedMaximumAmountOfMoneyToPresent = 100;
   
   private float _timePastSinceAppeared = 0;
   private float _timeCoolDown;
   
   private float _modifiedMinimumTimeDelay = 60.0f;
   private float _modifiedMaximumTimeDelay = 120.0f;
   
   private float _modifiedTimeToDisapear = 0;
   
   public void AdjustDifficultyDependedProperties()
   {
      int difficulty = (int)PlayerPrefs.GetFloat(PropertyTypes.Difficulty.ToString());

      _modifiedMaximumTimeDelay *= difficulty;
      _modifiedMinimumTimeDelay *= difficulty;

      _modifiedMinimumAmountOfMoneyToPresent /= difficulty;
      _modifiedMaximumAmountOfMoneyToPresent /= difficulty;

      _modifiedTimeToDisapear /= difficulty;
   }
   private void ProvideFinancialPresent()
   {
      _presentInstance = Instantiate(_presentPrefab);
      GlobalEventBus.Sync.Publish(this, new OnFinancialPresentAppeared());
      
      FinancialPresentController financialPresentController = _presentInstance.GetComponent<FinancialPresentController>();
      financialPresentController.SetMoneyToCollect = _amountOfMoneyToPresent;

      _timePastSinceAppeared = 0;
      _doesPresentExist = true;
   }

   private void GenerateNewPresentValues()
   {
      _amountOfMoneyToPresent = Random.Range(_minimumAmountOfMoneyToPresent, _maximumAmountOfMoneyToPresent);
      _timeCoolDown = Random.Range(_minimumTimeDelay, _maximumTimeDelay);
   }
   private void ProcessMoneyPresentCollected(object sender, EventArgs eventArgs)
   {
      _doesPresentExist = false;
      GenerateNewPresentValues();
      Invoke("ProvideFinancialPresent", _timeCoolDown);
   }

   private void Awake()
   {
      AdjustDifficultyDependedProperties();
   }

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnFinancialPresentClaimed>(ProcessMoneyPresentCollected);
   }
   private void OnDisable()
   {
      GlobalEventBus.Sync.Unsubscribe<OnFinancialPresentClaimed>(ProcessMoneyPresentCollected);
   }

   private void Start()
   {
      GenerateNewPresentValues();
      Invoke("ProvideFinancialPresent", _timeCoolDown);
   }
   private void FixedUpdate()
   {
      if (_doesPresentExist)
      {
         _timePastSinceAppeared += Time.deltaTime;
         if (_timePastSinceAppeared >= _timeToDisapear)
         {
            Destroy(_presentInstance);

            _presentInstance = null;
            _timePastSinceAppeared = 0;
            _doesPresentExist = false;
            
            GenerateNewPresentValues();
            Invoke("ProvideFinancialPresent", _timeCoolDown);
         }
      }
   }
   
    
}
