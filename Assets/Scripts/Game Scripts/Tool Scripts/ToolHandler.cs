using System;
using UnityEngine;

public class ToolHandler : MonoBehaviour
{
    [SerializeField] private ToolTypes _currentTool = ToolTypes.None;
    [SerializeField] private GameObject _moneyController;

    private MoneyController _moneyControllerComponent;
    private GameObject _triggeredTile;

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
        OnTileTriggered onTileTriggered = (OnTileTriggered)eventArgs;
        _triggeredTile = onTileTriggered.Tile;
        switch (_currentTool)
        {
            case ToolTypes.Bag:
                BagToolHandle();
                break;
            case ToolTypes.Shovel:
                ShovelToolHandle();
                break;
            case ToolTypes.Hoe:
                HoeToolHandle();
                break;
            case ToolTypes.Sickle:
                SickleToolHandle();
                break;
        }
    }
    private void BagToolHandle()
    {
        if (_triggeredTile.TryGetComponent<CultivatedDirt>(out var cultivatedDirt))
        {
            bool hasEnoughMoney = _moneyControllerComponent.CheckOperationProcessability(OperationTypes.Planting,
                cultivatedDirt.transform);
            if (hasEnoughMoney)
            {
                cultivatedDirt.Plant();
            }
        }
    }

    private void ShovelToolHandle()
    {
        if (_triggeredTile.TryGetComponent<GrassTile>(out var grass))
        {
            bool hasEnoughMoney =
                _moneyControllerComponent.CheckOperationProcessability(OperationTypes.Plowing, grass.transform);
            if (hasEnoughMoney)
            {
                grass.Plow();
            }
        }
    }
    private void HoeToolHandle()
    {
        if (_triggeredTile.TryGetComponent<DirtTile>(out var dirt))
        {
            dirt.Cultivate();
        }
    }

    private void SickleToolHandle()
    {
        if (_triggeredTile.TryGetComponent<CultivatedDirt>(out var cDirt))
        {
            cDirt.Crop();
        }
    }
}
