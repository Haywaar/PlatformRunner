using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChunksConfig", menuName = "ScriptableObjects/ChunksConfig", order = 1)]
public class ChunksConfig : ScriptableObject
{
    [SerializeField] private ChunkData _chunkData;

    public ChunkData ChunkData { get => _chunkData; }
}