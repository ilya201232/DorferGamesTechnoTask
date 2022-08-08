using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int minimumObjectsToCreate;
    [SerializeField] private int maximumObjectsToStoreInactive;
    private List<GameObject> _pool;

    private void Start()
    {
        _pool = new List<GameObject>();
        for (var i = 0; i < minimumObjectsToCreate; i++)
        {
            var newObject = Instantiate(objectPrefab, transform, true);
            newObject.transform.localRotation = quaternion.Euler(0, 0,0);
            newObject.SetActive(false);
            _pool.Add(newObject);
        }
    }

    public GameObject GetFreeObject()
    {
        // Clearing extra objects
        if (_pool.Count(obj => !obj.activeInHierarchy) > maximumObjectsToStoreInactive)
        {
            var extraObjects = new List<GameObject>();
            var count = 0;
            foreach (var obj in _pool.Where(obj => !obj.activeInHierarchy))
            {
                count++;

                if (count > maximumObjectsToStoreInactive)
                {
                    extraObjects.Add(obj);
                }
            }

            foreach (var extraObject in extraObjects)
            {
                _pool.Remove(extraObject);
                Destroy(extraObject);
            }
        }
        
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var obj in _pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.localRotation = quaternion.Euler(0, 0,0);
                return obj;
            }
                
        }

        var newObject = Instantiate(objectPrefab);
        newObject.transform.parent = transform;
        newObject.transform.localRotation = quaternion.Euler(0, 0,0);
        newObject.SetActive(false);
        _pool.Add(newObject);

        return newObject;
    }

    public void ResetAllObjects()
    {
        foreach (var obj in _pool.Where(obj => obj.activeInHierarchy))
        {
            obj.SetActive(false);
        }
    }
}