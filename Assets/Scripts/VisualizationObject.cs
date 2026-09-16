using UnityEngine;
using UnityEngine.UI;

public class VisualizationObject : MonoBehaviour
{
    [SerializeField] private Sprite[] _images;
    private Image _currentImage;
    private float _volumeStep;

    private void Awake()
    {
        _volumeStep = 100f / _images.Length;
        _currentImage = GetComponent<Image>();
    }
    public void SetValue(float textValue)
    {
        int index = Mathf.FloorToInt(textValue / _volumeStep);
        if (index >= _images.Length) index = _images.Length - 1;
        else if (index == 0 && textValue != 0) index++;
        _currentImage.sprite = _images[index];
    }
}