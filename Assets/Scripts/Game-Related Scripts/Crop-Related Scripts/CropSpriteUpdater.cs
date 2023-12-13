using UnityEngine;

public class CropSpriteUpdater : MonoBehaviour
{
    [SerializeField] 
    private Crop _typeOfCrop;
    private SpriteRenderer _bushSpriteRenderer;
    private void OnEnable()
    {
        _bushSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }
    public void SetSprite(int stage)
    {
        _bushSpriteRenderer.sprite = _typeOfCrop.GetBushGrowingStageSprite(stage);
    }

}
