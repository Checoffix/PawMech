using System;
using UnityEngine;
using UnityEngine.Events;

public class Parallax : MonoBehaviour
{
    [SerializeField] private ParalaxLayers[] _layers;
    [SerializeField] private UnityEvent _event;
    private Camera _camera;
    private Vector3 _startPos;
    private int _lastElemIndex;
    private int _currentLayer;
    private int _imagesCount;

    private void Start()
    {
        _camera = Camera.main;
        _startPos = transform.position;
        _imagesCount = _layers[_currentLayer].images.Length;
        _lastElemIndex = _imagesCount - 1;
    }
    void FixedUpdate()
    {
        if (_imagesCount != 0)
        {
            transform.position += new Vector3(_layers[_currentLayer].movementSpeed * Time.fixedDeltaTime, 0, 0);
            if (_layers[_currentLayer].images[_lastElemIndex].transform.position.x <= _camera.transform.position.x)
            {
                transform.position = _startPos;
            }
        }
    }

    public void ChangeLayer()
    {
        _event?.Invoke();
        foreach (var image in _layers[_currentLayer].images)
        {
            image.gameObject.SetActive(false);
        }
        _currentLayer = (int)Mathf.Repeat(_currentLayer + 1, _layers.Length);
        _imagesCount = _layers[_currentLayer].images.Length;
        _lastElemIndex = _imagesCount - 1;
        foreach (var image in _layers[_currentLayer].images)
        {
            image.gameObject.SetActive(true);
        }
    }
    [Serializable]
    private class ParalaxLayers
    {
        public SpriteRenderer[] images;
        public float movementSpeed;
    }
}
