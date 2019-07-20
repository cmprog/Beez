using UnityEngine;

public class SectionAllocator : MonoBehaviour
{
    public GameObject template;

    public Color color;
    public Color altColor;

    private void Start()
    {
        for (var r = -1; r <= 1; r++)
        {
            for (var c = -1; c <= 1; c++)
            {
                var section = Instantiate(this.template, this.transform);
                section.name = string.Format("Section ({0}, {1})", r, c);
                section.transform.Translate(c * section.transform.localScale.x, r * section.transform.localScale.y, 0);
                
                var reposition = section.GetComponent<SectionController>();
                reposition.basePosition = new Vector2Int(c, r);

                var renderer = section.GetComponent<SpriteRenderer>();
                renderer.color = ((r + c) % 2 == 0) ? this.color : this.altColor;
            }
        }
    }
}
