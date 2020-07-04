using RIU.MarchingCubes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMarching : MarchingCubes
{
    private List<Vector3> m_VerticesBuffer, m_NormalBuffer;
    private List<int> m_EdgeIndexBuffer, m_TrianglesBuffer;

    public SphereMarching(int cubeNumX, int cubeNumY, int cubeNumZ)
        :base(cubeNumX,cubeNumY,cubeNumZ)
    {
        m_VerticesBuffer = new List<Vector3>();
        m_NormalBuffer = new List<Vector3>();
        m_TrianglesBuffer = new List<int>();
        m_EdgeIndexBuffer = new List<int>();
    }

    public void ReMaker()
    {
        m_VerticesBuffer.Clear();
        m_NormalBuffer.Clear();
        m_TrianglesBuffer.Clear();
        m_EdgeIndexBuffer.Clear();
        March();
    }

    public bool smoothEnable { get; set; }

    public Vector3[] vertices => m_VerticesBuffer.ToArray();
    public Vector3[] normal => m_NormalBuffer.ToArray();
    public int[] triangles => m_TrianglesBuffer.ToArray();

    protected override float CalculateIos(Vector3 position)
    {
        return Vector3.Distance(sphere.position, position) / sphere.radius;
    }

    protected override void AddEdge(ref McEdge edge)
    {
        if (smoothEnable && m_EdgeIndexBuffer.Contains(edge.index))
            return;
        edge.vi = m_EdgeIndexBuffer.Count;
        m_EdgeIndexBuffer.Add(edge.index);
        m_VerticesBuffer.Add(edge.v3);
        m_NormalBuffer.Add(edge.n3);
    }

    protected override void AddTriangles(int vi1, int vi2, int vi3)
    {
        m_TrianglesBuffer.Add(vi1);
        m_TrianglesBuffer.Add(vi2);
        m_TrianglesBuffer.Add(vi3);
    }

    protected override void Interpolation(ref McEdge edge)
    {
        base.Interpolation(ref edge);
        float d = Vector3.Distance(edge.v3,sphere.position);
        edge.n3 = Vector3.Normalize(1.0f / (d * d) * (edge.v3 - sphere.position) * 2.0f);
    }

    public BoundingSphere sphere { get; set; }
}

public class SphereExample : MonoBehaviour
{
    public bool showCube, showPoint, showEdges, showNormal;
    public bool smoothEnable;
    [Range(3,10)]
    public int CubeCount;
    public SphereCollider sphere;

    private MeshFilter m_MeshFilter;
    private Mesh m_DynamicMesh;
    private SphereMarching m_SphereMarching;

    void Start()
    {
        m_MeshFilter = this.GetComponent<MeshFilter>();
        m_SphereMarching = new SphereMarching(CubeCount, CubeCount, CubeCount);
        m_DynamicMesh = new Mesh();
        m_DynamicMesh.MarkDynamic();
        m_MeshFilter.sharedMesh = m_DynamicMesh;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_SphereMarching.cubeNumX != CubeCount)
        {
            m_SphereMarching = new SphereMarching(CubeCount, CubeCount, CubeCount);
        }

        m_SphereMarching.smoothEnable = smoothEnable;

        m_SphereMarching.sphere = new BoundingSphere
        {
            radius = sphere.radius,
            position = sphere.transform.localPosition
        };
        m_SphereMarching.ReMaker();
        m_DynamicMesh.Clear();
        m_DynamicMesh.vertices = m_SphereMarching.vertices;
        m_DynamicMesh.triangles = m_SphereMarching.triangles;
        m_DynamicMesh.normals = m_SphereMarching.normal;
        if (!smoothEnable)m_DynamicMesh.RecalculateNormals();
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireCube(transform.localPosition,Vector3.one);
        if (m_SphereMarching == null)
            return;
        Gizmos.matrix = transform.localToWorldMatrix;
        if (showPoint)
        {
            Gizmos.color = new Color(0f,1f,0f,.5f);
            for(int i=0; i<m_SphereMarching.points.Length; i++)
            {
                var point = m_SphereMarching.points[i];
                Gizmos.DrawSphere(point.position, 0.01f);
            }
        }

        if (showCube)
        {
            Gizmos.color = new Color(.5f,.5f,.5f,.5f);
            for (int i = 0; i < m_SphereMarching.cubes.Length; i++)
            {
                Bounds b = m_SphereMarching.GetCubeBound(ref m_SphereMarching.cubes[i]);
                Gizmos.DrawWireCube(b.center, b.size);
            }
        }

        if(showEdges)
        {
            Gizmos.color = new Color(1f, 0f, 0f, .5f);
            for (int i = 0; i < m_SphereMarching.edges.Length; i++)
            {
                Gizmos.DrawLine(m_SphereMarching.points[m_SphereMarching.edges[i].pos[0]].position,
                    m_SphereMarching.points[m_SphereMarching.edges[i].pos[1]].position);
            }
        }

        if(showNormal)
        {
            Gizmos.color = new Color(0f, 1f, 0f, 1f);
            for (int i = 0; i < m_DynamicMesh.vertices.Length; i++)
            {
                Gizmos.DrawLine(m_DynamicMesh.vertices[i],
                    m_DynamicMesh.vertices[i] + m_DynamicMesh.normals[i] * 0.3f);
            }
        }
    }
}


