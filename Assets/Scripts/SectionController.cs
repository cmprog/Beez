using System.Collections.Generic;
using UnityEngine;

public class SectionController : MonoBehaviour
{
    private readonly List<GameObject> mActiveFlowerObjs = new List<GameObject>();
    private readonly List<GameObject> mActivePollenObjs = new List<GameObject>();

    private ObjectPool mPollenPool;
    private ObjectPool mFlowerPool;
    private WorldState mWorldState;
    private SpriteRenderer mSpriteRenderer;

    public Color evenColor;
    public Color oddColor;

    public Vector2Int basePosition;

    public Vector2Int previousVirtualPosition;
    public Vector2Int currentVirtualPosition;

    public float minPollenOffset;
    public float maxPollenOffset;

    private void Start()
    {
        var lGameController = FindObjectOfType<GameController>();
        this.mWorldState = lGameController.GameState.World;

        this.currentVirtualPosition = this.basePosition;
        this.previousVirtualPosition = this.basePosition;

        this.mSpriteRenderer = this.GetComponent<SpriteRenderer>();
        this.mSpriteRenderer.color = this.GetColor();

        this.mPollenPool = this.GetObjectPool(GameObjectTags.ObjectPools.Pollen);
        this.mFlowerPool = this.GetObjectPool(GameObjectTags.ObjectPools.Flower);

        this.PopulateSection();
    }

    private ObjectPool GetObjectPool(string tag)
    {
        var lGameObj = GameObject.FindGameObjectWithTag(tag);
        return lGameObj.GetComponent<ObjectPool>();
    }
    
    private void PopulateSection()
    {
        var lSectionState = this.mWorldState.GetSection(this.currentVirtualPosition.x, this.currentVirtualPosition.y);

        foreach (var lFlower in lSectionState.Flowers)
        {
            var lFlowerGameObj = this.AcquireAndRegister(this.mFlowerPool, this.transform, this.mActiveFlowerObjs);

            var lFlowerLocalScale = lFlowerGameObj.transform.localScale;

            var lPollenCounter = 0;
            foreach (var lPollen in lFlower.Pollen)
            {                
                var lPollenGameObj = this.AcquireAndRegister(this.mPollenPool, lFlowerGameObj.transform, this.mActivePollenObjs);

                var lPollenCollectable = lPollenGameObj.GetComponent<PollenCollectable>();
                lPollenCollectable.MaxPollen = double.MaxValue;
                lPollenCollectable.canCollect = true;
                lPollenCollectable.CurrentPollen = 1;

                var lBasePosition = new Vector3(lPollen.PositionX, lPollen.PositionY, 0);
                var lMagnitude = Random.Range(this.minPollenOffset, this.maxPollenOffset);
                lPollenGameObj.transform.localPosition = lBasePosition * lMagnitude;

                lPollenCounter++;
            }

            var lFlowerController = lFlowerGameObj.GetComponent<FlowerController>();
            lFlowerController.RemainingPollenCount = lPollenCounter;

            lFlowerGameObj.transform.localPosition = new Vector3(lFlower.PositionX, lFlower.PositionY, 0);
        }

        this.SetAllActive(this.mActiveFlowerObjs, true);
        this.SetAllActive(this.mActivePollenObjs, true);
    }
    
    private void SetAllActive(IEnumerable<GameObject> gameObjs, bool active)
    {
        foreach (var lGameObj in gameObjs)
        {
            lGameObj.SetActive(active);
        }
    }

    private GameObject AcquireAndRegister(ObjectPool pool, Transform parent, ICollection<GameObject> activeObjCollection)
    {
        var lGameObj = pool.Acquire(parent);
        activeObjCollection.Add(lGameObj);
        return lGameObj;
    }

    private void ReleaseAll()
    {
        this.ReleaseAll(this.mActivePollenObjs, this.mPollenPool);
        this.ReleaseAll(this.mActiveFlowerObjs, this.mFlowerPool);
    }

    private void ReleaseAll(List<GameObject> activeObjs, ObjectPool pool)
    {
        foreach (var obj in activeObjs)
        {
            pool.Release(obj);
        }

        activeObjs.Clear();
    }

    private void FixedUpdate()
    {
        var scale = this.transform.localScale;
        var shifted = false;

        var xThresholdOffset = scale.x * 1.5f;
        var xAbsShift = scale.x + scale.x + scale.x;

        var xVirtualPositionOffset = 0;
        var yVirtualPositionOffset = 0;
        var absPosShift = 3;

        var xTranslate = 0.0f;
        var yTranslate = 0.0f;

        if (this.transform.position.x < -xThresholdOffset)
        {
            xTranslate = xAbsShift;
            xVirtualPositionOffset = absPosShift;
            shifted = true;
        }
        else if (this.transform.position.x > xThresholdOffset)
        {
            xTranslate = -xAbsShift;
            xVirtualPositionOffset = -absPosShift;
            shifted = true;
        }

        var yThresholdOffset = scale.y * 1.5f;
        var yAbsShift = scale.y + scale.y + scale.y;

        if (this.transform.position.y < -yThresholdOffset)
        {
            yTranslate = yAbsShift;
            yVirtualPositionOffset = absPosShift;
            shifted = true;
        }
        else if (this.transform.position.y > yThresholdOffset)
        {
            yTranslate = -yAbsShift;
            yVirtualPositionOffset = -absPosShift;
            shifted = true;
        }

        if (shifted)
        {
            this.previousVirtualPosition = this.currentVirtualPosition;
            this.currentVirtualPosition.x += xVirtualPositionOffset;
            this.currentVirtualPosition.y += yVirtualPositionOffset;
            this.transform.Translate(xTranslate, yTranslate, 0.0f);

            this.mSpriteRenderer.color = this.GetColor();

            this.ReleaseAll();
            this.PopulateSection();
        }
    }

    private Color GetColor()
    {
        var lValue = this.currentVirtualPosition.x + this.currentVirtualPosition.y;
        return ((lValue % 2) == 0) ? this.evenColor : this.oddColor;
    }
}
