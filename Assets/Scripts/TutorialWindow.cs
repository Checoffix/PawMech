using UnityEngine;

public class TutorialWindow : MonoBehaviour
{
    [SerializeField] private Vector3[] _position;
    private int _currentIndex;
    private float width;
    private float cameraWidth;
    private bool _behind = false;

    private void Start()
    {
        var rect = GetComponent<RectTransform>();
        width = rect.sizeDelta.x * gameObject.transform.lossyScale.x / 2;
        cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }
    private void FixedUpdate()
    {
        if (transform.position.x - width < -cameraWidth)
        {
            _behind = true;
            UpdatePosition();
        }
        else if (_behind && transform.position.x - width + _position[0].x - _position[1].x > -cameraWidth)
        {
            _behind = false;
            UpdatePosition();
        }
    }
    private void UpdatePosition()
    {
        transform.localPosition = _position[_currentIndex];
        _currentIndex = (int)Mathf.Repeat(_currentIndex + 1, _position.Length);
    }
}
