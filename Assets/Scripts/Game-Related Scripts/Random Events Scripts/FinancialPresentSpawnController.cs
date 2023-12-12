using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FinancialPresentSpawnController : MonoBehaviour, IDifficultyDepended
{
   [SerializeField]private int _minimumAmountOfMoneyToPresent = 100;
   [SerializeField]private int _maximumAmountOfMoneyToPresent = 100;
   
   [SerializeField, Tooltip("In Seconds")]private float _minimumTimeDelay = 60.0f;
   [SerializeField, Tooltip("In Seconds")]private  float _maximumTimeDelay = 120.0f;
   
   [SerializeField] private float _timeToDisapear = 0;
   [SerializeField] private float _timePastSinceAppeared = 0;
   
   [SerializeField] private GameObject _presentPrefab;
   [SerializeField] private GameObject _presentInstance;
   [SerializeField] private Canvas _uiCanvas;

   [SerializeField] private bool _doesPresentExist = false;
   
   private int _amountOfMoneyToPresent;
   private float _timeCoolDown;
   
   public void AdjustDifficultyDependedProperties()
   {
      
   }
   private void ProvideFinancialPresent()
   {
      _presentInstance = Instantiate(_presentPrefab);
      //_presentInstance.transform.SetParent(_uiCanvas.transform);
      
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
   private void MoneyPresentClaimedHandler(object sender, EventArgs eventArgs)
   {
      _doesPresentExist = false;
      GenerateNewPresentValues();
      Invoke("ProvideFinancialPresent", _timeCoolDown);
   }

   private void OnEnable()
   {
      GlobalEventBus.Sync.Subscribe<OnFinancialPresentClaimed>(MoneyPresentClaimedHandler);
   }
   private void OnDisable()
   {
      GlobalEventBus.Sync.Unsubscribe<OnFinancialPresentClaimed>(MoneyPresentClaimedHandler);
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
