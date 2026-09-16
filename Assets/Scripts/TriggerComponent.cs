using UnityEngine;
using UnityEngine.Events;

public class TriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private UnityEvent<GameObject> _tagEvent;
    [SerializeField] private UnityEvent<GameObject> _layerEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!string.IsNullOrEmpty(_tag))
        {
            if (collision.gameObject.CompareTag(_tag))
            {
                _tagEvent.Invoke(collision.gameObject);
            }
        }
        if (_layerMask.value == (1 << collision.gameObject.layer))
        {
            _layerEvent?.Invoke(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!string.IsNullOrEmpty(_tag))
        {
            if (collision.gameObject.CompareTag(_tag))
            {
                _tagEvent.Invoke(collision.gameObject);
            }
        }
        if (_layerMask.value == (1 << collision.gameObject.layer))
        {
            _layerEvent?.Invoke(collision.gameObject);
        }
    }
}
