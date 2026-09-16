using System;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private SpawnElement[] _spawnElements;
    public GameObject Spawn(string id)
    {
        foreach (var elem in _spawnElements)
        {
            if (elem._id == id)
            {
                return Instantiate(elem._prefab, elem._target.position, new Quaternion());
            }
        }
        return null;
    }
    public GameObject Spawn(string id, Vector3 deltaPosition)
    {
        foreach (var elem in _spawnElements)
        {
            if (elem._id == id)
            {
                return Instantiate(elem._prefab, elem._target.position + deltaPosition, new Quaternion());
            }
        }
        return null;
    }
    public GameObject Spawn(string id, Quaternion rotation)
    {
        foreach (var elem in _spawnElements)
        {
            if (elem._id == id)
            {
                return Instantiate(elem._prefab, elem._target.position, rotation);
            }
        }
        return null;
    }
    public GameObject Spawn(string id, Vector3 newPosition, Quaternion rotation)
    {
        foreach (var elem in _spawnElements)
        {
            if (elem._id == id)
            {
                return Instantiate(elem._prefab, newPosition, rotation);
            }
        }
        return null;
    }
    public GameObject Spawn(GameObject gameObject)
    {
        foreach (var elem in _spawnElements)
        {
            if (elem._prefab == gameObject)
            {
                return Instantiate(elem._prefab, elem._target);
            }
        }
        return null;
    }
    public GameObject Spawn(GameObject gameObject, Vector3 newPosition)
    {
        foreach (var elem in _spawnElements)
        {
            if (elem._prefab == gameObject)
            {
                return Instantiate(elem._prefab, newPosition, new Quaternion());
            }
        }
        return null;
    }
    public void DieFx()
    {
        Spawn("die_fx");
    }
    public void HitFx()
    {
        Spawn("hit_fx");
    }
    [Serializable]
    private class SpawnElement
    {
        public string _id;
        public GameObject _prefab;
        public Transform _target;
    }
}
