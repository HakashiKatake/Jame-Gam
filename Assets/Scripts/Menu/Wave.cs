using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class Wave : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    [Header("Sittings")]
    [SerializeField] int Size;
    [SerializeField] int CellYOffset;
    [SerializeField] float Speed;
    float sinValue;
    [SerializeField] float WaveSize;
    Vector3 WaveValue;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    private void Update()
    {
        sinValue += Time.deltaTime * Speed;
        MakeMesh();
    }

    [ContextMenu("Create Mesh")]
    void MakeMesh()
    {
        CreateMesh();
        UpdateMesh();
    }

    void CreateMesh()
    {
        vertices = new Vector3[Size * 4];
        triangles = new int[Size * 6];

        float offset = 0;
        for (int i = 0; i < vertices.Length; i += 4)
        {
            WaveValue = new Vector3(Mathf.Sin(sinValue + (offset + 2f)) * WaveSize, 0, 0);
            Vector3 cellOffset = new Vector3(0, offset * CellYOffset, 0);
            vertices[i] = new Vector3(WaveValue.x, 0, 0) + cellOffset;
            vertices[i+1] = new Vector3(WaveValue.x, 1, 0) + cellOffset;
            vertices[i+2] = new Vector3(WaveValue.x + 1, 0, 0) + cellOffset;
            vertices[i + 3] = new Vector3(WaveValue.x + 1, 1, 0) + cellOffset;
            offset++;
        }

        int v = 0;
        for (int i = 0; i < triangles.Length; i += 6)
        {            
            triangles[i] = v;
            triangles[i + 1] = v + 1;
            triangles[i + 2] = v + 2;
            triangles[i + 3] = v + 2;
            triangles[i + 4] = v + 1;
            triangles[i + 5] = v + 3;
            
            v += 4;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
