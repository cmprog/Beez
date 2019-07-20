using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private Text timeText;

    public float startTime;
    public float remainingTime;

    public GameObject dayEndSummaryObj;
        
    void Start()
    {
        this.remainingTime = this.startTime;
        this.timeText = this.GetComponent<Text>();
    }

    private void Update()
    {
        this.remainingTime -= Time.deltaTime;

        if (this.remainingTime <= 0)
        {
            this.remainingTime = 0.0f;
            this.dayEndSummaryObj.gameObject.SetActive(true);
        }

        this.timeText.text = ((int)Mathf.Ceil(this.remainingTime)).ToString();
    }
}
