using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade : MonoBehaviour {
    
    private void Start()
    {
        Mesh mesh = GetComponent<Mesh>();
        
        Vector3[] vertices = mesh.vertices;

        int i = 0;
        while (i < vertices.Length)
        {
            Debug.Log( vertices[i].ToString() );
            i++;
        }

    }

}
