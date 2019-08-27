using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    //Init mesh object
    Mesh mesh;
    //Init vertex array
    Vector3[] vertices;
    //Init triangle array
    int[] triangles;

    //Init public grid size variables
    public int xSize = 20;
    public int zSize = 20;

    float perlinSF = 1;

    // Start is called before the first frame update
    void Start()
    {
        //Define mesh object
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void perlinSFValue(float updatedSF)
    {
        perlinSF = updatedSF;
        Debug.Log("PerlinSF = " + perlinSF);
    }

    // Update is called once per frame
    void Update()
    {
        CreateShape();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y * perlinSF, z);
                i++;
            }
        }

        int vert = 0;
        int tris = 0;

        triangles = new int[xSize * zSize * 6];

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }

        UpdateMesh();
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}

