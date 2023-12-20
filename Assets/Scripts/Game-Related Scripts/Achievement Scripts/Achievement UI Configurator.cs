
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUIConfigurator : MonoBehaviour
{
    [SerializeField] private TMP_Text _textToEdit;
    [SerializeField] private Slider _progressBar;

    private void OnEnable()
    {
        SetAchievementDescription("Collect 15 apples", 500);
        SetProgress(15, 5);
    }

    public void SetAchievementDescription(string description, int reward)
    {
        _textToEdit.text = $"{description} for ${reward}";
    }
    public void SetProgress(int maximumStage, int currentStage)
    {
        _progressBar.maxValue = maximumStage;
        _progressBar.value = currentStage;
    }
}
