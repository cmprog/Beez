using UnityEngine;

public class NonPlayerMovement : MonoBehaviour
{
    private PlayerSpeed mPlayerSpeed;
    private PlayerTurn mPlayerTurn;

    private void Start()
    {
        this.mPlayerSpeed = FindObjectOfType<PlayerSpeed>();
        this.mPlayerTurn = FindObjectOfType<PlayerTurn>();
    }

    private void FixedUpdate()
    {
        // x, y points to the direction the player is pointing, but we want
        // to move everything else in the opposite direction. Inverting the speed
        // so there's only one calculation being done.

        var offset = this.mPlayerTurn.Orientation * -this.mPlayerSpeed.currentSpeed;
        this.gameObject.transform.Translate(offset.x, offset.y, 0);
    }
}
