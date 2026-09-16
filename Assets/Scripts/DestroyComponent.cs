using UnityEngine;

public class DestroyComponent : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    public void Destroy()
    {
        Destroy(_gameObject);
    }
}
