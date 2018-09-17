using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Building")]
    public Texture texture;
    public Shader shader;
    public Color currentColor;
    public int levels;

    [Header("Roof")]
    public Texture roofTexture;

    [Header("Balcony")]
    public GameObject balconyPrefab;

    private void Start()
    {
        List<Vector2> vectors = new List<Vector2>()
        {
            new Vector2(0, 0),
            new Vector2(0, 5),
            new Vector2(2.5f, 7.5f),
            new Vector2(5, 5),
            new Vector2(5, 0),
            new Vector2(0, 0),
        };

        CreateBuilding(vectors, levels);
        CreateRoof(vectors, levels);
    }

    private void CreateBuilding(List<Vector2> points, float height)
    {
        // создаём пустой объект.
        GameObject go = new GameObject("Building");

        // добавляем MR.
        go.AddComponent<MeshRenderer>();

        // добавляем MF.
        MeshFilter mf = go.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        // лист вершин.
        List<Vector3> vertsList = new List<Vector3>();

        // добавляем вершины.
        for (int i = 0; i < points.Count; i++)
        {
            vertsList.Add(new Vector3(points[i].x, 0, points[i].y));
            //vertsList.Add(new Vector3(points[i].x, height / 10f, points[i].y));
            vertsList.Add(new Vector3(points[i].x, height, points[i].y));
        }

        // лист треугольников.
        List<int> tris = new List<int>();

        // кол-во треугольников.
        int trisCount = vertsList.Count - 2;

        // сдвиги.
        int shift = 0;

        // расчитываем треугольники.
        for (int i = 0; i < trisCount; i++)
        {
            for (int j = shift; j < shift + 3; j++)
                tris.Add(j);

            shift++;
        }

        // UV.
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < vertsList.Count; i++)
        {
            uvs.Add(new Vector2(vertsList[i].x, vertsList[i].y));
        }


        // переводим всё в массивы.
        Vector3[] verts = ListToArray<Vector3>(vertsList);
        int[] triangles = ListToArray<int>(tris);
        Vector2[] uv = ListToArray<Vector2>(uvs);

        // применяем настройки.
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // натягиваем текстуру.
        Renderer rend = go.GetComponent<Renderer>();
        rend.material.mainTexture = texture;
        rend.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        rend.material.mainTextureScale = new Vector2(1, 1);
        rend.material.color = currentColor;
        rend.material.shader = shader;

    }

    // крыша.
    private void CreateRoof(List<Vector2> points, float height)
    {
        // создаём пустой объект.
        GameObject go = new GameObject("Roof");
        go.transform.parent = GameObject.Find("Building").transform;

        // добавляем MR.
        go.AddComponent<MeshRenderer>();

        // добавляем MF.
        MeshFilter mf = go.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mf.mesh = mesh;

        // лист вершин.
        List<Vector2> vertsList = Triangulation.GetResult(points, true);

        // добавляем вершины.
        //for (int i = 0; i < points.Count; i++)
        //{
        //    vertsList.Add(new Vector3(points[i].x, height - 0.11f, points[i].y));
        //}



        // лист треугольников.
        List<int> tris = new List<int>();

        // кол-во треугольников.
        int trisCount = vertsList.Count - 2;

        // сдвиг.
        int shift = 0;

        // расчитываем треугольники.
        for (int i = 0; i < trisCount; i++)
        {
            for (int j = shift; j < shift + 3; j++)
                tris.Add(j);

            shift++;
        }


        // UV.
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < vertsList.Count; i++)
        {
            uvs.Add(new Vector2(vertsList[i].x, vertsList[i].y));
        }

        // переводим всё в массивы.
        Vector3[] verts = new Vector3[vertsList.Count];
        for (int i = 0; i < vertsList.Count; i++)
        {
            verts[i] = new Vector3(vertsList[i].x, height - 0.11f, vertsList[i].y);
        }
        int[] triangles = ListToArray<int>(tris);
        Vector2[] uv = ListToArray<Vector2>(uvs);

        // применяем настройки.
        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // натягиваем текстуру.
        Renderer rend = go.GetComponent<Renderer>();
        rend.material.mainTexture = roofTexture;
        rend.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        rend.material.mainTextureScale = new Vector2(1, 1);

    }

    private T[] ListToArray<T>(List<T> list)
    {
        T[] temp = new T[list.Count];
        for (int i = 0; i < list.Count; i++)
        {
            temp[i] = list[i];
        }

        return temp;
    }

}
