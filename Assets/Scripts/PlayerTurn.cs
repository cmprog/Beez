using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    private AttributeSet mAttributeSet;
    private float mActualTurnSpeed;
    private float mAbsoluteTurnSpeed;
    
    /// <summary>
    /// Setting this prevents jittle when the mouse isn't moving and the system is trying to
    /// make micro-angle adjustments.
    /// </summary>
    public float minThreshold;

    /// <summary>
    /// Gets the orientation as an angle in degrees. 0.0 points to the right (0, 1)
    /// </summary>
    public float OrientationDeg
    {
        get
        {
            return this.gameObject.transform.rotation.eulerAngles.z + 90;
        }
    }

    /// <summary>
    /// Gets the orientation as an angle in degrees. 0.0 points to the right (0, 1)
    /// </summary>
    public float OrientationRad
    {
        get
        {
            return Mathf.Deg2Rad * this.OrientationDeg;
        }
    }

    /// <summary>
    /// Gets the orietnation as a normalized vector
    /// </summary>
    public Vector2 Orientation
    {
        get
        {
            var rad = this.OrientationRad;
            var x = Mathf.Cos(rad);
            var y = Mathf.Sin(rad);
            return new Vector2(x, y);
        }
    }

    private void Start()
    {
        this.mAttributeSet = GameController.instance.Attributes;
        this.mAbsoluteTurnSpeed = this.mAttributeSet.GetSingle(AttributeKeys.BeeTurn);
    }

    private void FixedUpdate()
    {
        // player position is always (0, 0) so the mouse position always
        // represents the relative angle between the mouse and the player

        var mousePositionScreen = Input.mousePosition;
        var mousePositionRelative = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        var mouseAngleRad = Mathf.Atan2(mousePositionRelative.y, mousePositionRelative.x);

        // We add 90 here so that the math works out with (0, 1) at 0 degrees
        var lMouseOrientationDeg = (mouseAngleRad * Mathf.Rad2Deg);

        var lPlayerOrientationDeg = this.OrientationDeg;
        var lRelativeAngleDeg = Mathf.DeltaAngle(lPlayerOrientationDeg, lMouseOrientationDeg);

        if (Mathf.Abs(lRelativeAngleDeg) >= this.minThreshold)
        {
            if (lRelativeAngleDeg >= 0)
            {
                this.mActualTurnSpeed = this.mAbsoluteTurnSpeed;
            }
            else
            {
                this.mActualTurnSpeed = -this.mAbsoluteTurnSpeed;
            }

            this.transform.Rotate(0, 0, this.mActualTurnSpeed);
        }
    }
}
