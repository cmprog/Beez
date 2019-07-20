using System.Collections;
using UnityEngine;

public class SpinFlourish : MonoBehaviour
{
    private bool mIsActive = false;

    public float rate;
    public int rotationCount;

    public void Spin()
    {
        if (this.mIsActive) return;
        this.mIsActive = true;
        this.StartCoroutine(this.Execute());
    }

    private IEnumerator Execute()
    {
        var lCount = this.rotationCount * (360 / this.rate);
        for (var lIndex = 0; lIndex < lCount; lIndex++)
        {
            this.transform.Rotate(0, 0, this.rate);
            yield return new WaitForEndOfFrame();
        }
    }
}
