using UnityEngine;

public interface ISpawner
{
    void Generate(Vector3 position, Quaternion rotation);
}