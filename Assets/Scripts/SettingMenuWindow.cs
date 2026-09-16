using UnityEngine;

public class SettingMenuWindow : MonoBehaviour
{
    [SerializeField] private AudioSettingsWidget _music;
    [SerializeField] private AudioSettingsWidget _sfx;
    [SerializeField] private ScreenShakeToggle _screenShake;
    private void Start()
    {
        _music.SetModel(GameSettings.I.Music);
        _sfx.SetModel(GameSettings.I.Sfx);
        _screenShake.SetModel(GameSettings.I._screenShake);
    }
}
