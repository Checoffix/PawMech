using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private UnityEvent _QTEEvent;
    private PlayerMovement _playerMovement;
    private PlayerMakingSequence _playerSequence;
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerSequence = GetComponent<PlayerMakingSequence>();
    }
    public void OnMovement(InputAction.CallbackContext ctx)
    {
        var val = ctx.ReadValue<Vector2>();
        _playerMovement.SetMovement(val);
    }
    public void OnFirstSkill(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_playerSequence != null) _playerSequence.UseSkill(0);
        }
    }
    public void OnSecondSkill(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_playerSequence != null) _playerSequence.UseSkill(1);
        }
    }
    public void OnThirdSkill(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_playerSequence != null) _playerSequence.UseSkill(2);
        }
    }
    public void OnCast(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            if (_playerSequence != null) _playerSequence.Cast();
        }
    }
    public void OnClearSkills(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            if (_playerSequence != null) _playerSequence.DeleteSequence();
        }
    }
    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.canceled)
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            if (_pausePanel != null) _pausePanel.SetActive(!_pausePanel.activeSelf);
        }
    }
    public void OnQTE(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _QTEEvent?.Invoke();
        }
    }
}
