using UnityEngine;
using UnityEngine.Events;

public abstract class BaseSkill : MonoBehaviour
{
    [SerializeField] protected UnityEvent _onCast;
    public abstract bool Use();
}