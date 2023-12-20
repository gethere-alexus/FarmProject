using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
    [SerializeField] private Achievement[] _achievements;
    
    [SerializeField] private List<Achievement> _instantiatedAchievements;

    private void Start()
    {
        foreach (var achievement in _achievements)
        {
            _instantiatedAchievements.Add(Instantiate(achievement, this.gameObject.transform));
        }
    }

    public Achievement[] GetAchievementInformation()
    {
        return _instantiatedAchievements.ToArray();
    }
}
