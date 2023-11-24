using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
            _currentTool = onToolChosen.ChosenTool;
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
                case ToolTypes.Shovel:
                    if (onTileTriggered.Tile.TryGetComponent<Grass>(out var grass))
                    {
                        if (_moneyControllerComponent.CheckOperationProcessability(OperationTypes.Plowing))
                        {
                            grass.Plow();
                        }
                        else
                        {
                            
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
