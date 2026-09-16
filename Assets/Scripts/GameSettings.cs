using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private FloatPersistentProperty _music;
    [SerializeField] private FloatPersistentProperty _sfx;

    private static GameSettings _instance;
    public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

    public FloatPersistentProperty Music => _music;
    public FloatPersistentProperty Sfx => _sfx;

    public IntPersistentProperty _screenShake;

    public IntPersistentProperty _firstGame;

    private static GameSettings LoadGameSettings()
    {
        return _instance = Resources.Load<GameSettings>("GameSettings");
    }

    private void OnEnable()
    {
        _music = new FloatPersistentProperty(1, SoundSetting.Music.ToString());
        _sfx = new FloatPersistentProperty(1, SoundSetting.Sfx.ToString());
        _screenShake = new IntPersistentProperty(1, SoundSetting.ScreenShake.ToString());
        _firstGame = new IntPersistentProperty(1, SoundSetting.FirstGame.ToString());
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _music.Validate();
        _sfx.Validate();
        _screenShake.Validate();
        _firstGame.Validate();
    }
#endif
}
public enum SoundSetting
{
    Music,
    Sfx,
    ScreenShake,
    FirstGame
}
