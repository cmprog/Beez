using UnityEngine;

public sealed class FollowMovement : MonoBehaviour
{
    public GameObject target;

    public float speed;
    public float turn;

    public float minTurnThreshold;

    private void FixedUpdate()
    {
        var lTargetOffset = this.target.transform.position - this.transform.localPosition;
        var lTargetAngleRad = Mathf.Atan2(lTargetOffset.y, lTargetOffset.x);
        var lTargetAngleDeg = (lTargetAngleRad * Mathf.Rad2Deg) - 90;

        var lOffsetDeg = Mathf.DeltaAngle(this.transform.rotation.eulerAngles.z, lTargetAngleDeg);
        
        if (Mathf.Abs(lOffsetDeg) >= this.minTurnThreshold)
        {
            var lActualTurn = this.turn;
            if (lOffsetDeg < 0)
            {
                lActualTurn = -this.turn;
            }

            this.transform.Rotate(0, 0, lActualTurn);
        }

        var lOrientationDeg = this.transform.rotation.eulerAngles.z + 90;
        var lOrientationRad = Mathf.Deg2Rad * lOrientationDeg;
        var lOrientationX = Mathf.Cos(lOrientationRad);
        var lOrientationY = Mathf.Sin(lOrientationRad);
                
        this.transform.Translate(lOrientationX * this.speed, lOrientationY * this.speed, 0, Space.World);        
    }
}
