using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private AttributeSet mAttributeSet;
    private ObjectPool mPollenPool;
    private PollenCollectable mPollenCollectable;

    public double maximumPollen = 100.0f;

    public int PollenPickupCount { get; private set; }

    public double CurrentPollen
    {
        get { return this.mPollenCollectable.CurrentPollen; }
    }

    public float TotalDistanceTraveled { get; private set; }

    private void Start()
    {
        this.mAttributeSet = GameController.instance.Attributes;

        this.mPollenCollectable = this.GetComponent<PollenCollectable>();
        this.mPollenCollectable.canCollect = true;
        this.mPollenCollectable.MaxPollen = this.mAttributeSet.GetDouble(AttributeKeys.PollenCapacity);

        var pollenPoolObj = GameObject.FindGameObjectWithTag(GameObjectTags.ObjectPools.Pollen);
        this.mPollenPool = pollenPoolObj.GetComponent<ObjectPool>();
    }        

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var lCollectable = collision.GetComponent<PollenCollectable>();
        if (lCollectable != null)
        {
            if (this.mPollenCollectable.CollectFrom(lCollectable))
            {
                this.PollenPickupCount++;

                if (lCollectable.CompareTag(GameObjectTags.Pollen))
                {
                    this.mPollenPool.Release(collision.gameObject);

                    var lFlowerController = lCollectable.GetComponentInParent<FlowerController>();
                    if (lFlowerController != null)
                    {
                        lFlowerController.PollenCollected(this.mPollenCollectable);
                    }
                }
                
            }
        }
    }
}
