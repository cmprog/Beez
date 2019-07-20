using UnityEngine;

public class HiveTextMovement : MonoBehaviour
{
    private GameObject hiveObj;

    public Vector2 offset;

    private void Start()
    {
        this.hiveObj = GameObject.FindGameObjectWithTag(GameObjectTags.Hive);
    }

    private void Update()
    {
        var screenPos = Camera.main.WorldToScreenPoint(this.hiveObj.transform.position);
        this.transform.position = screenPos + (Vector3) this.offset;
    }
}
