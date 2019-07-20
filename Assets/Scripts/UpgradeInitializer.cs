using UnityEngine;

public class UpgradeInitializer : MonoBehaviour
{
    public GameObject template;

    private void Start()
    {
        var lUpgradeState = GameController.instance.GameState.Upgrades;
        foreach (var lCategory in lUpgradeState.Categories)
        {
            var lUpgradeDefinition = lUpgradeState.NextUpgradeForDefinition(lCategory);

            var lUpgradeObj = Instantiate(this.template);
            lUpgradeObj.transform.SetParent(this.transform, false);

            var lUpgradeController = lUpgradeObj.GetComponent<UpgradeController>();
            lUpgradeController.SetDefinition(lCategory, lUpgradeDefinition);
        }
    }
}
