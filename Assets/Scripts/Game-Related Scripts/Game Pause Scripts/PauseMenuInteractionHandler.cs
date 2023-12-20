using UnityEngine;

public class PauseMenuInteractionHandler : MonoBehaviour
{
    [SerializeField] private GameObject _achievementMenu;
    [SerializeField] private GameObject _gameObjectCanvas;
    public void ResumeGame()
    {
        GlobalEventBus.Sync.Publish(this, new OnGamePausePerformed(false));
        Destroy(_gameObjectCanvas);
    }

    public void OpenAchievementsMenu()
    {
        GlobalEventBus.Sync.Publish(this, new OnGamePauseMenuSwitched(Instantiate(_achievementMenu)));
        Destroy(_gameObjectCanvas);
    }
}
