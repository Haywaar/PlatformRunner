using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkData
{
    [SerializeField] private List<Chunk> _chunks;
    [SerializeField] private Chunk _startChunk;
    [SerializeField] private float _chunkLength = 32f;
    [SerializeField] private int _chunkPrewarmCount = 4;

    [Header("Lane coords")]
    [SerializeField] private float _leftLaneXPos;
    [SerializeField] private float _rightLaneXPos;
    [SerializeField] private float _middleLaneXPos;

    public List<Chunk> Chunks { get => _chunks; }
    public float ChunkLength { get => _chunkLength; }
    public Chunk StartChunk { get => _startChunk; }
    public int ChunkPrewarmCount { get => _chunkPrewarmCount; }

    public float LeftLaneXPos { get => _leftLaneXPos; }
    public float RightLaneXPos { get => _rightLaneXPos; }
    public float MiddleLaneXPos { get => _middleLaneXPos; }
}