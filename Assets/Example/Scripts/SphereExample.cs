using RIU.MarchingCubes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMarching : MarchingCubes
{
    public SphereMarching(int cubeNumX, int cubeNumY, int cubeNumZ)
        :base(cubeNumX,cubeNumY,cubeNumZ)
    {
    }
    
    public BoundingSphere sphere { get; set; }
    
    protected override float CalculateIossurface(Vector3 position)
    {
        return Vector3.Distance(sphere.position,position) / sphere.radius;
    }
}

public class SphereExample : MonoBehaviour
{
    public bool showCube, showPoint, showEdges;
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

        m_SphereMarching.sphere = new BoundingSphere
        {
            radius = sphere.radius,
            position = sphere.transform.localPosition
        };
        m_SphereMarching.March();
        m_DynamicMesh.Clear();
        m_DynamicMesh.vertices = m_SphereMarching.vertices;
        m_DynamicMesh.triangles = m_SphereMarching.triangles;
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
    }
}


