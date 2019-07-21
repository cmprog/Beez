using UnityEngine;

public class SectionAllocator : MonoBehaviour
{
    public GameObject template;

    public Color evenColor;
    public Color oddColor;

    private void Start()
    {
        for (var r = -1; r <= 1; r++)
        {
            for (var c = -1; c <= 1; c++)
            {
                var section = Instantiate(this.template);
                section.name = string.Format("Section ({0}, {1})", r, c);
                                
                var lSectionController = section.GetComponent<SectionController>();       
                lSectionController.basePosition = new Vector2Int(c, r);
                lSectionController.evenColor = this.evenColor;
                lSectionController.oddColor = this.oddColor;

                section.transform.SetParent(this.transform, false);
                section.transform.Translate(c * section.transform.localScale.x, r * section.transform.localScale.y, 0);
            }
        }
    }
}
