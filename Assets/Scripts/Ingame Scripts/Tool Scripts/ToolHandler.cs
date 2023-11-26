using System;
using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    [SerializeField]private ToolTypes _currentTool = ToolTypes.None;
    [SerializeField] private GameObject _moneyController;
    
    private MoneyController _moneyControllerComponent;
    private void OnEnable()
    {
        GlobalEventBus.Sync.Subscribe<OnToolChosen>(ToolChooseHandler);
        GlobalEventBus.Sync.Subscribe<OnTileTriggered>(TileHandler);
        
    }
    private void OnDisable()
    {
        GlobalEventBus.Sync.Unsubscribe<OnToolChosen>(ToolChooseHandler);
        GlobalEventBus.Sync.Unsubscribe<OnTileTriggered>(TileHandler);
    }

    private void Start()
    {
        _moneyControllerComponent = _moneyController.GetComponent<MoneyController>();
    }

    private void ToolChooseHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnToolChosen onToolChosen)
        {
            bool isChosenToolAlreadyActive = _currentTool == onToolChosen.ChosenTool;
            
            _currentTool = isChosenToolAlreadyActive ? ToolTypes.None : onToolChosen.ChosenTool;
            GlobalEventBus.Sync.Publish(this, new OnToolSwitched(_currentTool));
        }
    }

    private void TileHandler(object sender, EventArgs eventArgs)
    {
        if (eventArgs is OnTileTriggered onTileTriggered)
        {
            switch (_currentTool)
            {
                case ToolTypes.None:
                    break;
                case ToolTypes.Bag:
                    if (onTileTriggered.Tile.TryGetComponent<CultivatedDirt>(out var cultivatedDirt))
                    {
                        bool hasEnoughMoney = _moneyControllerComponent.CheckOperationProcessability(OperationTypes.Planting);
                        if (hasEnoughMoney)
                        {
                            cultivatedDirt.Plant();
                        }
                    }
                    break;
                case ToolTypes.Shovel:
                    if (onTileTriggered.Tile.TryGetComponent<Grass>(out var grass))
                    {
                        bool hasEnoughMoney = _moneyControllerComponent.CheckOperationProcessability(OperationTypes.Plowing);
                        if (hasEnoughMoney)
                        {
                            grass.Plow();
                        }
                    }
                    break;
                case ToolTypes.Hoe:
                    if (onTileTriggered.Tile.TryGetComponent<Dirt>(out var dirt))
                    {
                        dirt.Cultivate();
                    }
                    break;
                case ToolTypes.Sickle:
                    break;
                default:
                    break;
            }
        }
    }
}
