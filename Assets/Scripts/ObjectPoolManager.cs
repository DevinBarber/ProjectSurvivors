using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInformation> ObjectPools = new List<PooledObjectInformation>();

    public static GameObject SpawnObject(GameObject objPrefab, Vector3 spawnPos, Quaternion spawnRot)
    {
        PooledObjectInformation pool = null;

        foreach(PooledObjectInformation p in ObjectPools)
        {
            if(p.LookupString == objPrefab.name)
            {
                pool = p;
                break;
            }
        }

        if(pool == null)
        {
            pool = new PooledObjectInformation() { LookupString = objPrefab.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = null;
        foreach (GameObject obj in pool.InactiveObjects)
        {
            if(spawnableObj == null)
            {
                spawnableObj = obj;
                break;
            }
        }

        if(spawnableObj == null)
        {
            spawnableObj = Instantiate(objPrefab, spawnPos, spawnRot);
        } else
        {
            spawnableObj.transform.position = spawnPos;
            spawnableObj.transform.rotation = spawnRot;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string gameObjectName = obj.name.Substring(0, obj.name.Length - 7);
        PooledObjectInformation pool = ObjectPools.Find(p => p.LookupString == gameObjectName);

        if(pool == null)
        {
            Debug.Log("Lmao wrong");
        } else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }
}

public class PooledObjectInformation
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();
}
