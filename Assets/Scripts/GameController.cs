using UnityEngine;

public class GameController : MonoBehaviour
{    
    public static GameController instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }

        this.GameState = GameState.CreateNew();
    }

    public GameState GameState { get; private set; }

    public AttributeSet Attributes { get { return this.GameState.Attributes; } }
    
    public HiveState HiveState { get { return this.GameState.Hive; } }

    public WorldState WorldState { get { return this.GameState.World; } }    

    public void Save()
    {
        var lTargetFilePath = System.IO.Path.Combine(Application.persistentDataPath, "game.dat");
        using (var lTargetFileStream = System.IO.File.Open(lTargetFilePath, System.IO.FileMode.Create))
        {
            this.GameState.SaveTo(lTargetFileStream);
        }
    }
}
