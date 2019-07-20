using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    private string mCategory;
    private UpgradeDefinition mDefinition;
    private Button mButton;

    public Image icon; 

    public Text nameText;
    public Text descriptionText;
    public Text costText;

    private void Start()
    {
        this.mButton = this.GetComponent<Button>();
        this.UpdateButtonInteractable();
    }

    private void UpdateButtonInteractable()
    {
        if (this.mButton != null)
        {
            this.mButton.interactable = (this.mDefinition != null);
        }
    }

    public void SetDefinition(string category, UpgradeDefinition definition)
    {
        this.mCategory = category;
        this.mDefinition = definition;
        this.UpdateButtonInteractable();

        if (this.mDefinition != null)
        {
            this.nameText.text = this.mDefinition.Name;
            this.descriptionText.text = this.mDefinition.Description;
            this.costText.text = this.mDefinition.Cost.ToString("0.00");
        }
        else
        {
            this.nameText.text = "[MAXED]";
            this.descriptionText.text = "[MAXED]";
            this.costText.text = "[ALL]";
        }
    }

    public void Purchase()
    {
        if (this.mDefinition == null) return;

        var lGameState = GameController.instance.GameState;
        if (lGameState.Upgrades.Purchase(this.mCategory, lGameState))
        {
            var lNextDefinition = lGameState.Upgrades.NextUpgradeForDefinition(this.mCategory);
            this.SetDefinition(this.mCategory, lNextDefinition);
        }
        
    }
}
