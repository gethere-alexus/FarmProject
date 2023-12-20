using UnityEngine;

public class UIAchievementsLoader : MonoBehaviour
{
    [SerializeField] private AchievementUIConfigurator _achievementUIConfigurator;
    
    private AchievementHandler _achievementHandler;
    private void OnEnable()
    {
        _achievementHandler = GameObject.FindWithTag("AchievementsController").GetComponent<AchievementHandler>();
    }

    private void Start()
    {
        foreach (var achievement in _achievementHandler.GetAchievementInformation())
        {
            AchievementUIConfigurator instantiate = Instantiate(_achievementUIConfigurator, this.gameObject.transform);
            
            instantiate.SetAchievementDescription(achievement.AchievementDescription, achievement.Reward);
            instantiate.SetProgress((int)achievement.AmountOfSignalsToAchieve, (int)achievement.CurrentAmountOfSignals);
        }
    }
}
