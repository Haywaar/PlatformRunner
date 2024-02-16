using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "ScriptableObjects/LevelConfig", order = 1)]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private LevelData _levelData;

    public LevelData LevelData { get => _levelData; }
}