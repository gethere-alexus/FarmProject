using UnityEngine;

public class BushSpriteUpdater : MonoBehaviour
{
    private SpriteRenderer _bushSpriteRenderer;

    private string _lifeStagesFolder = "Sprites/BushLifeStages/";
    private void OnEnable()
    {
        _bushSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
    public void SetSprite(BushLifeStages lifeStage)
    {
        string spritePath = _lifeStagesFolder + lifeStage.ToString();
        _bushSpriteRenderer.sprite = Resources.Load<Sprite>(spritePath);
    }

}
