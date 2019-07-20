using UnityEngine;
using UnityEngine.UI;

public class PollenCollectable : MonoBehaviour
{
    public Text currentPollenText;
    public Text maxPollenText;

    [SerializeField]
    private double maxPollen;

    [SerializeField]
    private double currentPollen;

    public bool canCollect;

    private void Start()
    {
        this.UpdateCurrentPollenText();
        this.UpdateMaxPollenText();
    }

    public double MaxPollen
    {
        get { return this.maxPollen; }
        set
        {
            this.maxPollen = value;
            this.UpdateMaxPollenText();
        }
    }

    public double CurrentPollen
    {
        get { return this.currentPollen; }
        set
        {
            this.currentPollen = value;
            this.UpdateCurrentPollenText();
        }
    }

    private void UpdateMaxPollenText()
    {
        if (this.maxPollenText != null)
        {
            this.maxPollenText.text = this.ToString(this.MaxPollen);
        }
    }

    private void UpdateCurrentPollenText()
    {
        if (this.currentPollenText != null)
        {
            this.currentPollenText.text = this.ToString(this.CurrentPollen);
        }
    }

    private string ToString(double value)
    {
        return value.ToString("0.00");
    }

    public bool AddPollen(double amount)
    {
        if (this.CurrentPollen < this.MaxPollen)
        {
            this.CurrentPollen += amount;

            if (this.CurrentPollen > this.MaxPollen)
            {
                this.CurrentPollen = this.MaxPollen;
            }

            if (this.currentPollenText != null)
            {
                this.currentPollenText.text = this.ToString(this.CurrentPollen);
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Collects the pollen from <paramref name="other"/> and adds it to this collectable.
    /// </summary>
    /// <returns>true if collection occured (pollen count changed); otherwise false.</returns>
    public bool CollectFrom(PollenCollectable other)
    {
        if (!other.canCollect) return false;
        if (other.CurrentPollen <= 0) return false;

        if (this.AddPollen(other.CurrentPollen))
        {
            other.CurrentPollen = 0.0;
            return true;
        }

        return false;
    }
}
