using RIU.MarchingCubes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBallsMarching : MarchingCubes
{
    private List<Vector3> m_VerticesBuffer, m_NormalBuffer;
    private List<int> m_TrianglesBuffer;
    private int m_EdgeVerticesIndex;

    public MetaBallsMarching(int cubeNumX, int cubeNumY, int cubeNumZ)
    : base(cubeNumX, cubeNumY, cubeNumZ)
    {
        m_VerticesBuffer = new List<Vector3>();
        m_NormalBuffer = new List<Vector3>();
        m_TrianglesBuffer = new List<int>();
    }

    public void ReMaker()
    {
        m_VerticesBuffer.Clear();
        m_NormalBuffer.Clear();
        m_TrianglesBuffer.Clear();
        m_EdgeVerticesIndex = 0;
        March();
    }

    public Vector3[] vertices => m_VerticesBuffer.ToArray();
    public Vector3[] normal => m_NormalBuffer.ToArray();
    public int[] triangles => m_TrianglesBuffer.ToArray();

    protected override void AddEdge(ref McEdge edge)
    {
        if (edge.vi != -1)
            return;
        edge.vi = m_EdgeVerticesIndex++;
        m_VerticesBuffer.Add(edge.v3);
        m_NormalBuffer.Add(edge.n3);
    }

    protected override void AddTriangles(int vi1, int vi2, int vi3)
    {
        m_TrianglesBuffer.Add(vi1);
        m_TrianglesBuffer.Add(vi2);
        m_TrianglesBuffer.Add(vi3);
    }

    protected override float CalculateIos(Vector3 position)
    {
        float ios = 0f;
        for (int i = 0; i < blobs.Length; i++)
        {
            ios += Vector3.Distance(new Vector3(blobs[i].x, blobs[i].y, blobs[i].z),position) / blobs[i].w;
        }
        return ios;
    }

    protected override void Interpolation(ref McEdge edge)
    {
        base.Interpolation(ref edge);

        Vector3 result = Vector3.zero;
        for (int i = 0; i < blobs.Length; i++)
        {
            Vector3 bp = new Vector3(blobs[i].x, blobs[i].y, blobs[i].z);
            float d = Vector3.Distance(edge.v3, bp);
            result += Vector3.Normalize(1.0f / (d * d) * (edge.v3 - bp) * 2.0f);
        }
        edge.n3 = result.normalized;
        edge.uv = new Vector2(edge.v3.x * 0.5f + 0.5f, edge.v3.y * 0.5f + 0.5f);
    }

    public Vector4[] blobs{ get; set; }
}

public class MetaBallsExample : MonoBehaviour
{
    [Range(10, 50)]
    public int CubeCount;
    public MetaBallsMarching metaBalls;

    private MeshFilter m_MeshFilter;
    private Mesh m_DynamicMesh;

    private Vector4[] m_Blobs;

    void Start()
    {
        m_MeshFilter = this.GetComponent<MeshFilter>();
        metaBalls = new MetaBallsMarching(CubeCount, CubeCount, CubeCount);
        m_DynamicMesh = new Mesh();
        m_DynamicMesh.MarkDynamic();
        m_MeshFilter.sharedMesh = m_DynamicMesh;

        m_Blobs = new Vector4[5]
        {
            new Vector4(.16f, .26f, .16f, .33f),
            new Vector4(.13f, -.134f, .35f, .32f),
            new Vector4(-.18f, .125f, -.25f, .36f),
            new Vector4(-.13f, .23f, .255f, .33f),
            new Vector4(-.18f, .125f, .35f, .32f)
        };
    }

    void Update()
    {

        m_Blobs[0].x = .12f + .12f * (float)Mathf.Sin((float)Time.time * .50f);
        m_Blobs[0].y = .06f + .23f * (float)Mathf.Cos((float)Time.time * .2f);
        m_Blobs[1].x = .12f + .12f * (float)Mathf.Sin((float)Time.time * .2f);
        m_Blobs[1].y = -.23f + .10f * (float)Mathf.Cos((float)Time.time * 1f);
        m_Blobs[2].y = -.03f + .24f * (float)Mathf.Sin((float)Time.time * .35f);
        m_Blobs[3].y = .126f + .10f * (float)Mathf.Cos((float)Time.time * .1f);
        m_Blobs[4].x = .206f + .1f * (float)Mathf.Cos((float)Time.time * .5f);
        m_Blobs[4].y = .056f + .2f * (float)Mathf.Sin((float)Time.time * .3f);
        m_Blobs[4].z = .25f + .08f * (float)Mathf.Cos((float)Time.time * .2f);

        transform.Rotate(Time.deltaTime * 10f, 0, Time.deltaTime * .6f);

        if (metaBalls.cubeNumX != CubeCount)
        {
            metaBalls = new MetaBallsMarching(CubeCount, CubeCount, CubeCount);
        }
        metaBalls.blobs = m_Blobs;
        metaBalls.ReMaker();
        m_DynamicMesh.Clear();
        m_DynamicMesh.vertices = metaBalls.vertices;
        m_DynamicMesh.triangles = metaBalls.triangles;
        m_DynamicMesh.normals = metaBalls.normal;
    }
}
