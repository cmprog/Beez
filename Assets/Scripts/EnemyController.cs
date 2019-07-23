using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private FollowMovement mMovement;
    private bool mIsFollowingZapper;
    private ObjectPool mEnemyPool;

    public GameObject bugZappersParent;

    public float timeRemaining;

    private void Start()
    {
        this.mEnemyPool = ObjectPool.FindEnemyObjectPool();
        this.mMovement = this.GetComponent<FollowMovement>();
    }

    private void FixedUpdate()
    {
        if (this.mIsFollowingZapper) return;

        this.timeRemaining -= Time.deltaTime;

        if (this.timeRemaining < 0)
        {
            var lZapperIndex = Random.Range(0, this.bugZappersParent.transform.childCount);
            var lZapperTransform = this.bugZappersParent.transform.GetChild(lZapperIndex);
            this.mMovement.target = lZapperTransform.gameObject;
            this.mIsFollowingZapper = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameObjectTags.Player))
        {
            var lPollenCollectable = collision.GetComponent<PollenCollectable>();
            lPollenCollectable.CurrentPollen *= 0.5;

            this.mEnemyPool.Release(this.gameObject);
        }
    }
}
