using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OverheatQTESegments : MonoBehaviour
{
    [SerializeField] private Image[] _firstGo;
    [SerializeField] private Image[] _secondGo;
    [SerializeField] private RectTransform _pointerRect;
    [SerializeField] private UnityEvent _onQTESuccessEvent;
    private int _firstSegmentIndex, _secondSegmentIndex;

    public void ChooseRandomSegments()
    {
        _firstSegmentIndex = Random.Range(0, _firstGo.Length - 1);
        _firstGo[_firstSegmentIndex].enabled = true;
        _secondSegmentIndex = Random.Range(0, _secondGo.Length - 1);
        _secondGo[_secondSegmentIndex].enabled = true;
    }
    public void HideRandomSegments()
    {
        _firstGo[_firstSegmentIndex].enabled = false;
        _secondGo[_secondSegmentIndex].enabled = false;
    }

    public void CheckOverlaps()
    {
        if (_firstGo[_firstSegmentIndex].gameObject.GetComponent<RectTransform>().GetWorldRect().Overlaps(_pointerRect.GetWorldRect()))
        {
            _onQTESuccessEvent?.Invoke();
            HideRandomSegments();
        }
        else if (_secondGo[_secondSegmentIndex].gameObject.GetComponent<RectTransform>().GetWorldRect().Overlaps(_pointerRect.GetWorldRect()))
        {
            _onQTESuccessEvent?.Invoke();
            HideRandomSegments();
        }
    }
}
