using System.Collections;
using UnityEngine;
public class HighlightScript : MonoBehaviour
{
    [SerializeField] private float _fadePause = 0.15f;

    private SpriteRenderer[] _spriteRenderer;
    private MaterialPropertyBlock _materialPropertyBlock;
    private float _blinkFactor;
    private Coroutine _coroutine;

    private void Start()
    {
        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void Highlight()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(HighlightWait());
    }

    private IEnumerator HighlightWait()
    {
        foreach (var renderer in _spriteRenderer)
        {
            renderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetFloat("_BlinkFactor", 1);
            renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        yield return new WaitForSeconds(_fadePause);

        foreach (var renderer in _spriteRenderer)
        {
            renderer.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetFloat("_BlinkFactor", 0);
            renderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}
