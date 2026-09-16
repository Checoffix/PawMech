using UnityEngine;

public class ChangeParallax : MonoBehaviour
{
    [SerializeField] private GameObject _changeImage;
    [SerializeField] private Parallax[] _parallaxes;
    [SerializeField] private int _changeSpeed;
    private bool _hasChanged = false;
    private float _changeImageSize;
    private float _deltaToEvent;
    private Vector3 startPos;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        startPos = _changeImage.transform.position;
        _changeImageSize = _changeImage.GetComponent<SpriteRenderer>().bounds.size.x;
    }
    public void OnEnable()
    {
        _deltaToEvent = 0;
        _hasChanged = false;
        enabled = true;
    }

    private void FixedUpdate()
    {
        _changeImage.transform.position += new Vector3(_changeSpeed * Time.fixedDeltaTime, 0, 0);
        if (_changeImage.transform.position.x + _deltaToEvent <= _camera.transform.position.x)
        {
            if (!_hasChanged)
            {
                _hasChanged = true;
                _deltaToEvent = _changeImageSize;
                foreach (var parallax in _parallaxes)
                {
                    parallax.ChangeLayer();
                }
            }
            else
            {
                _changeImage.transform.position = startPos;
                enabled = false;
            }
        }
    }
}
