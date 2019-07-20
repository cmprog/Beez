using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DayEndSummary : MonoBehaviour
{
    private float mSavedTimeScale;

    public Text dayCountText;
    public Text collectedPollenText;
    public Text salvagedPollenText;
    public Text honeyBatchesText;
    public Text processedPollenText;
    public Text generatedHoneyText;
    
    private void OnEnable()
    {
        var lHiveController = FindObjectOfType<HiveController>();
        var lPlayerController = FindObjectOfType<PlayerController>();

        this.mSavedTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;

        var lGameState = GameController.instance.GameState;

        var lFinalizationInput = new DaySummaryInput();
        lFinalizationInput.CollectedPollenPickups = lPlayerController.PollenPickupCount;
        lFinalizationInput.CollectedPollen = lHiveController.CurrentPollen;
        lFinalizationInput.HiveDropOffs = lHiveController.PollenDropOffCount;        
        lFinalizationInput.RemainingBeePollen = lPlayerController.CurrentPollen;
        lFinalizationInput.TraveledDistance = lPlayerController.TotalDistanceTraveled;

        // TODO
        lFinalizationInput.FlowersCollected = 0;        
        lFinalizationInput.FurthestDistanceTraveled = 0;

        var lResult = lGameState.FinializeDay(lFinalizationInput);
        GameController.instance.Save();
        
        this.dayCountText.text = lResult.DayNumber.ToString();
        this.collectedPollenText.text = lResult.CollectedPollen.ToString("0.00");
        this.salvagedPollenText.text = lResult.SalvagedPollen.ToString("0.00");
        this.honeyBatchesText.text = lResult.BatchesProcessed.ToString();
        this.processedPollenText.text = lResult.PollenUsed.ToString("0.00");
        this.generatedHoneyText.text = lResult.GeneratedHoney.ToString("0.00");
    }

    public void StartNextDay()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = this.mSavedTimeScale;
    }
}
