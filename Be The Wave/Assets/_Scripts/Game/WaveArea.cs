using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveArea : MonoBehaviour {

    public Material m_sparkMaterial;

	// Use this for initialization
	void Start () {
        // Box
        BoxCollider box = GetComponent<BoxCollider>();

        Vector3 corner1 = transform.position - box.size/2 + box.center;
        Vector3 corner2 = transform.position + box.size/2 + box.center;

        CreateSparkle(corner1);
        CreateSparkle(corner2);

        
        for (float x = corner1.x; x < corner2.x; x += 1f) {
            for (float y = corner1.y; y < corner2.y; x += 1f) {
                for (float z = corner1.z; z < corner2.z; z += 1f) {
                    CreateSparkle(new Vector3(x, y, z));
                }
            }
        }

        Debug.Log(corner1.ToString() + " " + corner2.ToString());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateSparkle(Vector3 position) {
        GameObject sparkle = new GameObject("Sparkle");

        MeshFilter meshFilter = sparkle.AddComponent<MeshFilter>();
        meshFilter.mesh = CreatePlaneMesh(0.1f, 0.1f);

        MeshRenderer renderer = sparkle.AddComponent<MeshRenderer>();
        renderer.material = m_sparkMaterial;

        sparkle.transform.parent = transform;
        sparkle.transform.position = position;
        sparkle.transform.transform.rotation = Camera.main.transform.rotation;
    }

    // Source: http://answers.unity3d.com/questions/139808/creating-a-plane-mesh-directly-from-code.html
    Mesh CreatePlaneMesh(float width = 1, float height = 1) {
        Mesh m = new Mesh();
        m.name = "ScriptedMesh";
        m.vertices = new Vector3[] {
         new Vector3(-width, -height, 0.01f),
         new Vector3(width, -height, 0.01f),
         new Vector3(width, height, 0.01f),
         new Vector3(-width, height, 0.01f)
     };
        m.uv = new Vector2[] {
         new Vector2 (0, 0),
         new Vector2 (0, 1),
         new Vector2(1, 1),
         new Vector2 (1, 0)
     };
        m.triangles = new int[] { 0, 2, 1, 0, 3, 2 };
        m.RecalculateNormals();

        return m;
    }
}
