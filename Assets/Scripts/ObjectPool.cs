using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// Make sure something isn't released back to the pool twice
    /// </summary>
    private readonly HashSet<GameObject> mAvailableObjSet = new HashSet<GameObject>();
    private readonly List<GameObject> mAvailableObjs = new List<GameObject>();
    private int mNextInstanceId = 0;

    public GameObject template;
    public int initialCount;

    public string nameFormat;
    
    private string NextName()
    {
        var lName = string.Format(this.nameFormat, this.mNextInstanceId);
        this.mNextInstanceId++;
        return lName;
    }

    private GameObject CreateNewObject(Transform parent)
    {
        var lGameObj = Instantiate(this.template);
        lGameObj.transform.SetParent(parent, true);
        lGameObj.name = this.NextName();
        lGameObj.SetActive(false);
        return lGameObj;
    }
    
    private void Start()
    {
        mAvailableObjs.Capacity = this.initialCount;
        for (var lCounter = 0; lCounter < this.initialCount; lCounter++)
        {            
            this.mAvailableObjs.Add(this.CreateNewObject(this.transform));
        }
    }

    public GameObject Acquire(Transform parent)
    {
        if (this.mAvailableObjs.Count > 0)
        {
            var lIndex = this.mAvailableObjs.Count - 1;
            var lGameObj = this.mAvailableObjs[lIndex];

            this.mAvailableObjs.RemoveAt(lIndex);
            this.mAvailableObjSet.Remove(lGameObj);

            lGameObj.transform.SetParent(parent, true);

            return lGameObj;
        }

        return this.CreateNewObject(parent);
    }

    public void Release(GameObject obj)
    {
        if (this.mAvailableObjSet.Add(obj))
        {
            obj.SetActive(false);
            this.mAvailableObjs.Add(obj);
        }
    }

    public static ObjectPool FindEnemyObjectPool()
    {
        return FindByTag(GameObjectTags.ObjectPools.Enemy);
    }

    public static ObjectPool FindByTag(string tag)
    {
        var lGameObject = GameObject.FindGameObjectWithTag(tag);
        return lGameObject.GetComponent<ObjectPool>();
    }
}
