using UnityEngine;

public class BugZapperController : MonoBehaviour
{
    private ObjectPool mBugPoool;

    private void Start()
    {
        this.mBugPoool = ObjectPool.FindEnemyObjectPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameObjectTags.Bug))
        {
            this.mBugPoool.Release(collision.gameObject);
        }        
    }
}
