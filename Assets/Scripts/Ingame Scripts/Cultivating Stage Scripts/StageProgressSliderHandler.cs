using System;
using UnityEngine;
using UnityEngine.UI;

public class StageProgressSliderHandler : MonoBehaviour
{
    [SerializeField] private GameObject _sliderPrefab;
    [SerializeField] private Vector3 _sliderPositionFromTileOffset;
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnDirtCultivatingStageCompleted>(SliderCreationHandler);
    }

    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnDirtCultivatingStageCompleted>(SliderCreationHandler);
    }

    private void SliderCreationHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnDirtCultivatingStageCompleted onDirtCultivatingStageCompleted)
        {
            if (onDirtCultivatingStageCompleted.isTheFirstCultivating)
            {
                GameObject createdPrefab = Instantiate(_sliderPrefab);
                Slider createdSlider = createdPrefab.GetComponentInChildren<Slider>();

                Vector3 tilePosition = onDirtCultivatingStageCompleted.CultivatedTile.transform.position;
                Vector3 sliderPosition = tilePosition + _sliderPositionFromTileOffset;
            
                createdPrefab.gameObject.transform.position = sliderPosition;

                createdSlider.maxValue = onDirtCultivatingStageCompleted.MaxStages;
                createdSlider.value = onDirtCultivatingStageCompleted.CurrentStage;
                createdSlider.minValue = 0;

                createdPrefab.transform.SetParent(onDirtCultivatingStageCompleted.CultivatedTile.transform);
            }
            else
            {
                GameObject sliderPrefab = onDirtCultivatingStageCompleted.CultivatedTile.transform.GetChild(0).gameObject;
                Slider createdSlider = sliderPrefab.GetComponentInChildren<Slider>();

                createdSlider.value = onDirtCultivatingStageCompleted.CurrentStage;
                if (onDirtCultivatingStageCompleted.isCultivationCompleted)
                {
                    onDirtCultivatingStageCompleted.CultivatedTile.GetComponent<Dirt>().ChangeToCultivatedDirt();
                }
            }
        }
    }
}
