using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameObjectTags.Player))
        {
            var lPollenCollectable = collision.GetComponent<PollenCollectable>();
            lPollenCollectable.CurrentPollen *= 0.5;

            Destroy(this.gameObject);
        }
    }
}
