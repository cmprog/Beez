using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private DayManager mDayManager;
    private ObjectPool mEnemyPool;
    private GameObject mPlayer;

    private float mNextSpawnTimer;
    public GameObject spawnPointsParent;
    public GameObject bugZappersParent;

    private void Start()
    {
        var lGameState = GameController.instance.GameState;
        this.mDayManager = lGameState.DayManager;

        this.mEnemyPool = ObjectPool.FindEnemyObjectPool();

        this.mPlayer = GameObject.FindGameObjectWithTag(GameObjectTags.Player);
    }

    private void FixedUpdate()
    {
        this.mNextSpawnTimer -= Time.deltaTime;
        while (this.mNextSpawnTimer <= 0)
        {
            this.SpawnEnemy();

            var lTimerOverflow = this.mNextSpawnTimer;
            // TODO: Calculate current distance from hive
            this.mNextSpawnTimer = (float) this.mDayManager.NextTimeToEnemy(0);
            this.mNextSpawnTimer += lTimerOverflow;
        }
    }

    private void SpawnEnemy()
    {
        var lSpawnPoint = this.NextSpawnPoint();

        var lEnemy = this.mEnemyPool.Acquire(this.transform);
        lEnemy.transform.position = lSpawnPoint.position;

        var lMovement = lEnemy.GetComponent<FollowMovement>();
        lMovement.target = this.mPlayer;

        var lController = lEnemy.GetComponent<EnemyController>();
        lController.bugZappersParent = this.bugZappersParent;        

        lEnemy.SetActive(true);
    }

    private Transform NextSpawnPoint()
    {
        var lIndex = Random.Range(0, this.spawnPointsParent.transform.childCount);
        return this.spawnPointsParent.transform.GetChild(lIndex);        
    }
}
