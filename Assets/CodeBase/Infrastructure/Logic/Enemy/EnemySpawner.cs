using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour, ISpawner
{
    public Enemy prefab;
    public int enemiesCount;
    public float xOffset;
    public float zOffset;

    private float _randomPosX;
    private float _randomPosZ;

    private void Start()
    {
        Generate(transform.position, Quaternion.identity);
    }

    public void Generate(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            _randomPosX = Random.Range(-xOffset, xOffset);
            _randomPosZ = Random.Range(-zOffset, zOffset);
            
            Instantiate(prefab, position + new Vector3(_randomPosX, 0, _randomPosZ), rotation);
        }
    }
}