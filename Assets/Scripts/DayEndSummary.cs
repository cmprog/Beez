using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayEndSummary : MonoBehaviour
{
    private float mSavedTimeScale;

    public GameObject formFieldTemplate;
    
    private void OnEnable()
    {
        this.PauseGame();

        var lHiveController = FindObjectOfType<HiveController>();
        var lPlayerController = FindObjectOfType<PlayerController>();
        
        var lGameState = GameController.instance.GameState;

        var lDayStats = StatisticsSet.CreateEmpty();
        lDayStats.Set(StatisticKeys.PollenPickupCount, lPlayerController.PollenPickupCount);
        lDayStats.Set(StatisticKeys.PollenCollected, lHiveController.CurrentPollen);
        lDayStats.Set(StatisticKeys.HiveDropOffCount, lHiveController.PollenDropOffCount);
        lDayStats.Set(StatisticKeys.RemainingPollen, lPlayerController.CurrentPollen);
        lDayStats.Set(StatisticKeys.TotalDistanceTraveled, lPlayerController.TotalDistanceTraveled);

        // TODO
        lDayStats.Set(StatisticKeys.TotalFlowersCollected, 0);
        lDayStats.Set(StatisticKeys.FurthestDistanceTraveled, 0);

        lGameState.FinializeDay(lDayStats);
        GameController.instance.Save();

        this.AddFormField("Collected Pollen", lDayStats.GetString(StatisticKeys.PollenCollected, "0.00"));
        this.AddFormField("Salvaged Pollen", lDayStats.GetString(StatisticKeys.SalvagedPollen, "0.00"));
        this.AddFormField("Available Pollen", lGameState.Statistics.GetString(StatisticKeys.RemainingPollen, "0.00"));
        this.AddFormField("Batches Processed", lDayStats.GetString(StatisticKeys.HoneyBatchCount, "0.00"));
        this.AddFormField("Pollen Used", lDayStats.GetString(StatisticKeys.PollenProcessed, "0.00"));        
        this.AddFormField("Honey Produced", lDayStats.GetString(StatisticKeys.HoneyProduced, "0.00"));
        this.AddFormField("Total Honey", lGameState.Statistics.GetString(StatisticKeys.HoneyAvailable, "0.00"));
    }

    private void PauseGame()
    {
        this.mSavedTimeScale = Time.timeScale;
        Time.timeScale = 0.0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = this.mSavedTimeScale;
    }

    private GameObject FindChildWithTag(Transform parent, string tag)
    {
        for (var lIndex = 0; lIndex < parent.childCount; lIndex++)
        {
            var lChildTransform = parent.GetChild(lIndex);
            if (lChildTransform.CompareTag(tag)) return lChildTransform.gameObject;

            var lChildObj = this.FindChildWithTag(lChildTransform, tag);
            if (lChildObj != null) return lChildObj;
        }        

        return null;
    }

    private void AddFormField(string header, string value)
    {   
        var lGameObj = Instantiate(this.formFieldTemplate);
        this.SetFormFieldText(lGameObj, GameObjectTags.FormField.Header, header);
        this.SetFormFieldText(lGameObj, GameObjectTags.FormField.Value, value);
        lGameObj.transform.SetParent(this.transform, false);
    }

    private void SetFormFieldText(GameObject parent, string tag, string value)
    {
        var lFormFieldObj = this.FindChildWithTag(parent.transform, tag);
        var lTextElement = lFormFieldObj.GetComponent<Text>();
        lTextElement.text = value;
    }

    public void StartNextDay()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        this.ResumeGame();
    }
}
