using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveArea : MonoBehaviour {

    public Material m_sparkMaterial;

    private List<GameObject> m_sparkleList = new List<GameObject>();

    private float[] m_waves = new float[10];

	// Use this for initialization
	void Start () {
        // Box
        BoxCollider box = GetComponent<BoxCollider>();

        Vector3 corner1 = -box.size/2 + box.center;
        Vector3 corner2 = box.size/2 + box.center;

        

        float step = 0.15f;
        for (float x = corner1.x; x < corner2.x; x += step) {
            for (float y = corner1.y; y < corner2.y; y += step) {
                for (float z = corner1.z; z < corner2.z; z += step) {
                    CreateSparkle(new Vector3(x + step / 2 + Random.Range(-(step/2), step/2), y + step / 2 + Random.Range(-(step / 2), step / 2), z + step / 2 + Random.Range(-(step / 2), step / 2)));
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

        for(int w = 0; w < 10; w++) {
            if (m_waves[w] < 3) m_waves[w] += 5 * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            for (int w = 0; w < 10; w++) {
                if (m_waves[w] > 3) {
                    m_waves[w] = 0;
                    break;
                }
            }
        }

        foreach (GameObject sparkle in m_sparkleList) {
            float distance = Vector3.Distance(sparkle.transform.position, transform.position);
            sparkle.transform.localScale = new Vector3(1,1,1) * getIntensity(distance);
        }
	}

    float getIntensity(float distance) {

        float sum = 0f;

        for (int w = 0; w < 10; w++) {
            if (m_waves[w] < 3) {
                sum += Mathf.Clamp(1 - (Mathf.Abs(distance - m_waves[w])) * 1 - (m_waves[w] / 2), 0, 1);
                //sum += (3 / (m_waves[w] - 1)) / Mathf.Pow((distance - m_waves[w]), 2);
            }
        }

        

        return sum;
    }

    void CreateSparkle(Vector3 position) {
        GameObject sparkle = new GameObject("Sparkle");

        MeshFilter meshFilter = sparkle.AddComponent<MeshFilter>();
        meshFilter.mesh = CreatePlaneMesh(0.05f, 0.05f);

        MeshRenderer renderer = sparkle.AddComponent<MeshRenderer>();
        renderer.material = m_sparkMaterial;

        sparkle.transform.parent = transform;
        sparkle.transform.localPosition = position;
        sparkle.transform.rotation = Camera.main.transform.rotation;
        sparkle.transform.localScale = new Vector3(0, 0, 0);

        m_sparkleList.Add(sparkle);
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
