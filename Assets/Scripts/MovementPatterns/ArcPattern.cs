using UnityEngine;

[CreateAssetMenu(menuName = "MovementPatterns/ArcPattern", fileName = "ArcPattern")]
public class ArcPattern : MovementPattern
{
    private static ArcPattern _instance;
    public static ArcPattern I => _instance == null ? LoadPattern() : _instance;

    private static ArcPattern LoadPattern()
    {
        return _instance = Resources.Load<ArcPattern>("MovementPatterns/ArcPattern");
    }

    public AnimationCurve _directionCurve;
    public AnimationCurve _speedCurve;
    public float _yOffset;
    public float _xVelocity;
    private Vector2 _previousVector;

    public override Vector2 GetVelocity(float t)
    {
        if (t > 1) return _previousVector * Speed;
        else
        {
            if (_maxDuration == 5)
            {
                Debug.Log(_directionCurve.Evaluate(0));
                Debug.Log(_directionCurve.Evaluate(1));
                Debug.Log(_directionCurve.Evaluate(2));
            }
            _previousVector = new Vector2(_xVelocity, _directionCurve.Evaluate(t) + _yOffset);
            return _previousVector * _speedCurve.Evaluate(t) * Speed;
        }
    }
}
