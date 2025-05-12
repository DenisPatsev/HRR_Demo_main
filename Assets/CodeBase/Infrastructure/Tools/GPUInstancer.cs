using System.Collections.Generic;
using UnityEngine;

public class GPUInstancer : MonoBehaviour
{
    [SerializeField] private Transform[] _trees1Positions;
    [SerializeField] private Transform[] _trees2Positions;
    [SerializeField] private Transform[] _fernsPositions;

    [SerializeField] private Mesh _tree1Mesh;
    [SerializeField] private Mesh _tree2Mesh;
    [SerializeField] private Mesh _fernMesh;

    [SerializeField] private Material _treesMaterial;
    [SerializeField] private Material _fernMaterial;

    private List<List<Matrix4x4>> _tree1Matrices = new List<List<Matrix4x4>>();
    private List<List<Matrix4x4>> _tree2Matrices = new List<List<Matrix4x4>>();
    private List<List<Matrix4x4>> _fernsMatrices = new List<List<Matrix4x4>>();

    // private MaterialPropertyBlock _treesMaterialPropertyBlock;
    // private MaterialPropertyBlock _fernsMaterialPropertyBlock;

    private void Start()
    {
        GenerateTrees1();
        GenerateTrees2();
        GenerateFerns();
        // CreateBuffer();
    }

    private void Update()
    {
        DrawTrees1();
        DrawTrees2();
        DrawFerns();
    }

    // private void OnDisable()
    // {
    //     if(_tree1Buffer != null)
    //         _tree1Buffer.Release();
    //     
    //     if(_tree2Buffer != null)
    //         _tree2Buffer.Release();
    //     
    //     if(_fernbuffer != null)
    //         _fernbuffer.Release();
    //     
    //     _tree1Buffer = null;
    //     _tree2Buffer = null;
    //     _fernbuffer = null;
    // }

    private void GenerateTrees1()
    {
        // _treesMaterialPropertyBlock = new MaterialPropertyBlock();
        List<Matrix4x4> list = new List<Matrix4x4>();

        for (int i = 0; i < _trees1Positions.Length; i++)
        {
            if (i % 1023 == 0)
            {
                if (list.Count > 0)
                    _tree1Matrices.Add(list);
                
                list = new List<Matrix4x4>();
            }

            Vector3 position = _trees1Positions[i].position;
            Quaternion rotation = Quaternion.Euler(_trees1Positions[i].rotation.eulerAngles);
            Vector3 scale = _trees1Positions[i].lossyScale;
            list.Add(Matrix4x4.TRS(position, rotation, scale));
        }
        // _tree1Matrices[i] = Matrix4x4.TRS(position, rotation, scale);
    }

    private void DrawTrees1()
    {
        // Graphics.DrawMeshInstanced(_tree1Mesh, 0, _treesMaterial, _tree1Matrices);
        foreach (var matrices in _tree1Matrices)
        {
            Graphics.DrawMeshInstanced(_tree1Mesh, 0, _treesMaterial, matrices);
        }
    }

    private void GenerateTrees2()
    {
        // _treesMaterialPropertyBlock = new MaterialPropertyBlock();

        List<Matrix4x4> list = new List<Matrix4x4>();

        for (int i = 0; i < _trees2Positions.Length; i++)
        {
            if (i % 1023 == 0)
            {
                if (list.Count > 0)
                    _tree2Matrices.Add(list);
                
                list = new List<Matrix4x4>();
            }

            Vector3 position = _trees2Positions[i].position;
            Quaternion rotation = Quaternion.Euler(_trees2Positions[i].rotation.eulerAngles);
            Vector3 scale = _trees2Positions[i].lossyScale;
            list.Add(Matrix4x4.TRS(position, rotation, scale));
        }
    }

    private void DrawTrees2()
    {
        // Graphics.DrawMeshInstanced(_tree2Mesh, 0, _treesMaterial, _tree2Matrices);
        foreach (var matrices in _tree2Matrices)
        {
            Graphics.DrawMeshInstanced(_tree2Mesh, 0, _treesMaterial, matrices);
        }
    }

    private void GenerateFerns()
    {
        // _fernsMaterialPropertyBlock = new MaterialPropertyBlock();

        List<Matrix4x4> list = new List<Matrix4x4>();

        for (int i = 0; i < _fernsPositions.Length; i++)
        {
            if (i % 1023 == 0)
            {
                if (list.Count > 0)
                {
                    _fernsMatrices.Add(list);
                    Debug.Log("Added");
                }

                list = new List<Matrix4x4>();
            }

            Vector3 position = _fernsPositions[i].position;
            Quaternion rotation = Quaternion.Euler(_fernsPositions[i].rotation.eulerAngles);
            Vector3 scale = _fernsPositions[i].lossyScale;
            Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);
            list.Add(matrix);
        }
    }

    private void DrawFerns()
    {
        // Graphics.DrawMeshInstanced(_fernMesh, 0, _fernMaterial, _fernsMatrices);
        foreach (var matrices in _fernsMatrices)
        {
            Graphics.DrawMeshInstanced(_fernMesh, 0, _fernMaterial, matrices);
        }
    }

    // private void CreateBuffer()
    // { 
    //     _tree1Buffer = new ComputeBuffer(_trees1Positions.Length, 16 * sizeof(float));
    //     _tree2Buffer = new ComputeBuffer(_trees2Positions.Length, 16 * sizeof(float));
    //     _fernbuffer = new ComputeBuffer(_fernsPositions.Length, 16 * sizeof(float));
    //     
    //     _tree1Buffer.SetData(_tree1Matrices);
    //     _tree2Buffer.SetData(_tree2Matrices);
    //     _fernbuffer.SetData(_fernsMatrices);
    // }
}