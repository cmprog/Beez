using UnityEngine;
using UnityEngine.UI;

public class HiveController : MonoBehaviour
{
    private PollenCollectable mPollenCollectable;

    public Text collectedPollenText;

    public int PollenDropOffCount { get; private set; }

    public double CurrentPollen
    {
        get { return this.mPollenCollectable.CurrentPollen; }
    }

    private void Start()
    {
        this.mPollenCollectable = this.GetComponent<PollenCollectable>();
        this.mPollenCollectable.MaxPollen = double.MaxValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameObjectTags.Player))
        {
            var lPollenCollectable = collision.GetComponent<PollenCollectable>();
            if (this.mPollenCollectable.CollectFrom(lPollenCollectable))
            {
                this.PollenDropOffCount++;
            }
        }
    }
}
