using UnityEngine;

public class FlowerController : MonoBehaviour
{
    private AttributeSet mAttributes;
    private SpinFlourish mSpinFlourish;

    public int RemainingPollenCount { get; set; }

    private void Start()
    {
        this.mAttributes = GameController.instance.GameState.Attributes;
        this.mSpinFlourish = this.GetComponent<SpinFlourish>();
    }

    public void PollenCollected(PollenCollectable collectable)
    {
        this.RemainingPollenCount--;
        if (this.RemainingPollenCount == 0)
        {
            collectable.AddPollen(this.mAttributes.GetDouble(AttributeKeys.FlowerBonus));
            this.mSpinFlourish.Spin();
        }
    }
}
