using System;
using UnityEngine;
using UnityEngine.UI;

public class CropSelecter : MonoBehaviour
{
    [SerializeField] 
    private GameObject[] _availableCropsToPlant;
    private GameObject _selectedCrop;
    
    [SerializeField] private Sprite[] _cropIcons;
    [SerializeField] private Image _iconContainer;
    
    private int _selectedCropIndex = 0;

    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnCultivatedDirtAppeared>(SetSelectedTileOnAppearedTile);
    }
    private void Start()
    {
        _selectedCrop = _availableCropsToPlant[_selectedCropIndex];
        _iconContainer.sprite = _cropIcons[_selectedCropIndex];
        GlobalEventBus.Sync.Publish(this, new OnNewCropChosen(_selectedCrop));
    }
    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnCultivatedDirtAppeared>(SetSelectedTileOnAppearedTile);
    }
    private void SetSelectedTileOnAppearedTile(object sender, EventArgs eventArgs)
    {
        CultivatedDirt cultivatedDirt = (CultivatedDirt)sender;
        cultivatedDirt.SetActiveCrop(_selectedCrop);
    }
    public void SelectNextCrop()
    {
        _selectedCropIndex = _selectedCropIndex + 1 >= _availableCropsToPlant.Length ? 0 : _selectedCropIndex + 1;
        _selectedCrop = _availableCropsToPlant[_selectedCropIndex];
        _iconContainer.sprite = _cropIcons[_selectedCropIndex];
        
        GlobalEventBus.Sync.Publish(this, new OnNewCropChosen(_selectedCrop));
    }
    public void SelectPreviousCrop()
    {
        _selectedCropIndex = _selectedCropIndex - 1 < 0 ? _availableCropsToPlant.Length - 1 : _selectedCropIndex - 1;
        _selectedCrop = _availableCropsToPlant[_selectedCropIndex];
        _iconContainer.sprite = _cropIcons[_selectedCropIndex];
        
        GlobalEventBus.Sync.Publish(this, new OnNewCropChosen(_selectedCrop));
    }
}
