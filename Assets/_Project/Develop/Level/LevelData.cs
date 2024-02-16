using UnityEngine;

[System.Serializable]
public struct LevelData
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _timeUntilMaxSpeed;
    [SerializeField] private float _speedUpKoef;
    [SerializeField] private float _speedUpKoefLength;
    [SerializeField] private float _slowDownKoef;
    [SerializeField] private float _slowDownKoefLength;
    [Header("Lane coords")]
    [SerializeField] private float _leftLaneXPos;
    [SerializeField] private float _rightLaneXPos;
    [SerializeField] private float _middleLaneXPos;
    

    public float MinSpeed { get => _minSpeed; set => _minSpeed = value; }
    public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }
    /// <summary>
    /// Время за которое на уровне скорость от minSpeed дорастает до maxSpeed
    /// </summary>
    public float TimeUntilMaxSpeed { get => _timeUntilMaxSpeed; }
    public float SpeedUpKoef { get => _speedUpKoef; }
    public float SlowDownKoef { get => _slowDownKoef; }
    public float SpeedUpKoefLength { get => _speedUpKoefLength; }
    public float SlowDownKoefLength { get => _slowDownKoefLength; }
    public float LeftLaneXPos { get => _leftLaneXPos; }
    public float RightLaneXPos { get => _rightLaneXPos; }
    public float MiddleLaneXPos { get => _middleLaneXPos; }
}