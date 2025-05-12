using UnityEngine;
using Random = UnityEngine.Random;

public class TreesGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _tree1;
    [SerializeField] private GameObject _tree2;
    [SerializeField] private GameObject _fern;
    [Range(0, 3000f)] public int _trees1Count;
    [Range(0, 3000f)] public int _trees2Count;
    [Range(0, 5000f)] public int _fernsCount;
    [SerializeField] private float _randomPositionRange;
    [SerializeField] private float _minimalFernScale;
    [SerializeField] private float _minimalTreesScale;

    private void Awake()
    {
        GenerateTrees();
        GenerateFerns();
    }
    
    private void GenerateTrees()
    {
        for (int i = 0; i < _trees1Count; i++)
        {
            var tree = Instantiate(_tree1, transform);
            tree.transform.localPosition = new Vector3(Random.Range(-_randomPositionRange, _randomPositionRange),
                tree.transform.position.y, Random.Range(-_randomPositionRange, _randomPositionRange));
            var scale = Random.Range(_minimalTreesScale, 1);
            tree.transform.localScale = new Vector3(scale, scale, scale);
        }

        for (int i = 0; i < _trees2Count; i++)
        {
            var tree = Instantiate(_tree2, transform);
            tree.transform.localPosition = new Vector3(Random.Range(-_randomPositionRange, _randomPositionRange),
                tree.transform.position.y, Random.Range(-_randomPositionRange, _randomPositionRange));
            var scale = Random.Range(_minimalTreesScale, 1);
            tree.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private void GenerateFerns()
    {
        for (int i = 0; i < _fernsCount; i++)
        {
            var tree = Instantiate(_fern, transform);
            tree.transform.localPosition = new Vector3(Random.Range(-_randomPositionRange, _randomPositionRange),
                tree.transform.position.y, Random.Range(-_randomPositionRange, _randomPositionRange));
            var scale = Random.Range(_minimalFernScale, 0.8f);
            tree.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}