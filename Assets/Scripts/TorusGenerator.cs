using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class TorusGenerator : MonoBehaviour
{
    public int radialSegments = 24;
    public int tubularSegments = 12;
    public float radius = 1f;
    public float tubeRadius = 0.05f;

    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh = GenerateTorusMesh();
    }

    Mesh GenerateTorusMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(radialSegments + 1) * (tubularSegments + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[radialSegments * tubularSegments * 6];

        int vert = 0, tri = 0;

        for (int i = 0; i <= radialSegments; i++)
        {
            float u = (float)i / radialSegments * Mathf.PI * 2;
            Vector3 center = new Vector3(Mathf.Cos(u) * radius, 0, Mathf.Sin(u) * radius);

            for (int j = 0; j <= tubularSegments; j++)
            {
                float v = (float)j / tubularSegments * Mathf.PI * 2;
                Vector3 normal = new Vector3(Mathf.Cos(v) * Mathf.Cos(u), Mathf.Sin(v), Mathf.Cos(v) * Mathf.Sin(u));
                vertices[vert] = center + normal * tubeRadius;
                uv[vert] = new Vector2((float)i / radialSegments, (float)j / tubularSegments);

                if (i < radialSegments && j < tubularSegments)
                {
                    int a = vert;
                    int b = vert + tubularSegments + 1;
                    int c = vert + 1;
                    int d = vert + tubularSegments + 2;

                    triangles[tri++] = a;
                    triangles[tri++] = b;
                    triangles[tri++] = c;

                    triangles[tri++] = c;
                    triangles[tri++] = b;
                    triangles[tri++] = d;
                }

                vert++;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }
}
