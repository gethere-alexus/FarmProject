using UnityEngine;
using UnityEngine.SceneManagement;

public enum ButtonTypes 
{
    PlayButton,SettingsButton,QuitButton, 
    PreviousSceneButton, LoadGameButton, 
    CreateGameButton, GenerateNewWorldButton,
    ResumeGameButton
}
public class ButtonClickNotifier : MonoBehaviour
{
    [SerializeField] private ButtonTypes buttonType;

    public void SendButtonClickMessage()
    {
        GlobalEventBus.Sync.Publish(this, new OnButtonPressed(buttonType, SceneManager.GetActiveScene().name));
    }
}