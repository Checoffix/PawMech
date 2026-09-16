using System;
using UnityEngine;
using UnityEngine.Events;

public class QTEMovement : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 endPos;
    [SerializeField] private float speed;
    [SerializeField] private UnityEvent _onLooseQTE;
    private Vector2 _nextPos;
    public void StartQTE()
    {
        transform.localPosition = startPos;
        gameObject.SetActive(true);
    }
    private void FixedUpdate()
    {
        _nextPos = transform.localPosition;
        _nextPos.x -= speed;
        transform.localPosition = _nextPos;
        if (Math.Abs(_nextPos.x - endPos.x) <= speed)
        {
            _onLooseQTE?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void StopQTE()
    {
        gameObject.SetActive(false);
    }
}
