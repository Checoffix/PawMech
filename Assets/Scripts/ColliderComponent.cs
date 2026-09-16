using UnityEngine;
using UnityEngine.Events;

public class ColliderComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent<GameObject> _tagEvent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!string.IsNullOrEmpty(_tag))
        {
            if (collision.gameObject.CompareTag(_tag))
            {
                _tagEvent.Invoke(collision.gameObject);
            }
        }
    }
}
