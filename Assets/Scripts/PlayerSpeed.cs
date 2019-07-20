using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    private AttributeSet mAttributeSet;
    private PlayerController mPlayerController;

    public float maxSpeed;
    public float currentSpeed;

    public float minRadius;
    public float maxRadius;
    public float currentRadius;
    
    private void Start()
    {
        this.mAttributeSet = GameController.instance.Attributes;
        this.mPlayerController = this.GetComponent<PlayerController>();

        this.maxSpeed = this.mAttributeSet.GetSingle(AttributeKeys.BeeSpeed);
        this.currentSpeed = this.maxSpeed;
    }

    private void FixedUpdate()
    {
        var lMousePositionScreen = Input.mousePosition;
        // Need to case to a Vector2 otherwise the megnitude calculation includes the z axis
        var lMousePositionWorld = (Vector2) Camera.main.ScreenToWorldPoint(lMousePositionScreen);

        this.currentRadius = lMousePositionWorld.magnitude;

        var lRelative = Mathf.Clamp01((this.currentRadius - this.minRadius) / (this.maxRadius - this.minRadius));
        this.currentSpeed = Mathf.Lerp(0, this.maxSpeed, lRelative);
    }
}
