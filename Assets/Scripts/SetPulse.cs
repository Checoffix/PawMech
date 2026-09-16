using UnityEngine;

public class SetPulse : MonoBehaviour
{
    [SerializeField] private GameObject textR;
    [SerializeField] private float _maxSize;
    [SerializeField] private float _minSize;
    [SerializeField] private float _growFactor;
    private bool _isUp = true;
    private float _currentSize;
    private void FixedUpdate()
    {
        textR.transform.localScale = Vector3.one * _currentSize;
        if (_isUp) _currentSize += _growFactor;
        else _currentSize -= _growFactor;
        if (_currentSize >= _maxSize) _isUp = false;
        else if (_currentSize <= _minSize) _isUp = true;
    }

    private void OnEnable()
    {
        textR.SetActive(true);
        _currentSize = textR.transform.localScale.x;
    }
    private void OnDisable()
    {
        textR.SetActive(false);
    }
}
