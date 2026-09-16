using UnityEngine;

[CreateAssetMenu(menuName = "MovementPatterns/LinearPattern", fileName = "LinearPattern")]
public class LinearPattern : MovementPattern
{
    private static LinearPattern _instance;
    public static LinearPattern I => _instance == null ? LoadPattern() : _instance;

    private static LinearPattern LoadPattern()
    {
        return _instance = Resources.Load<LinearPattern>("MovementPatterns/LinearPattern");
    }

    public Vector2 _direction;
    public override Vector2 GetVelocity(float t)
    {
        return _direction * Speed;
    }
}
