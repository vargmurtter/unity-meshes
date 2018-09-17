using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    public Texture texture;
    public Shader shader;

    void Start () {


        MeshRenderer mr = GetComponent<MeshRenderer>();
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        Vector3[] verts = new Vector3[]
        {
            new Vector3(0, 0, 0),           // 0
            new Vector3(0, 0, 1),           // 1
            new Vector3(0.5f, 0, 1.5f),     // 2
            new Vector3(1, 0, 1),           // 3
            new Vector3(1, 0, 0),           // 4
        };

        int[] triangles = new int[]
        {
            1, 2, 3,
            3, 4, 0,
            0, 1, 3
        };

        mesh.vertices = verts;
        mesh.triangles = triangles;

        Vector2[] uvs = new Vector2[verts.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].z);
        }
        mesh.uv = uvs;

        // натягиваем текстуру.
        Renderer rend = GetComponent<Renderer>();
        rend.material.mainTexture = texture;
        rend.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        rend.material.mainTextureScale = new Vector2(1, 1);
        rend.material.shader = shader;


    }
}
