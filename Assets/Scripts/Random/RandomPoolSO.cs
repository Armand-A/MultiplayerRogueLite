using System.Collections.Generic;
using UnityEngine;

// extend this SO with a concrete type and CreateAssetMenu that way
public class RandomPoolSO<T> : ScriptableObject
{
    [SerializeField] List<T> poolContent = new List<T>();

    List<T> runtimePool;

    private void OnEnable()
    {
        runtimePool = new List<T>(poolContent);
    }

    private void OnValidate()
    {
        OnEnable();
    }

    public bool IsPoolEmpty() => runtimePool.Count == 0;
    public int PoolCount => runtimePool.Count;

    // doesn't always get count number of elements
    // if count > PoolCount, return all elements in pool instead (less than count, equal PoolCount)
    public List<T> Get(int count)
    {
        int rollCount = Mathf.Min(runtimePool.Count, count);
        if (rollCount == runtimePool.Count) return runtimePool;

        List<T> results = new List<T>();
        while (results.Count < rollCount)
        {
            T obj = runtimePool[Random.Range(0, runtimePool.Count)];
            if (results.Contains(obj)) continue;
            results.Add(obj);
        }
        return results;
    }

    public bool Ban(T objectToBan)
    {
        return runtimePool.Remove(objectToBan);
    }
}
