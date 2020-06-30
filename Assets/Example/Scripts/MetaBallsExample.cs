using RIU.MarchingCubes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBallsExample : MonoBehaviour
{
    public int xCubeCount = 10,yCubeCount = 10,zCubeCount = 10;
    
    private MetaBalls m_MetaBalls;
    private float[][] m_Blobs;

    // Start is called before the first frame update
    void Start()
    {
        m_Blobs = new float[1][];
        m_Blobs[0] = new float[] { 0f, 0f, 0f, .5f };
        //m_Blobs[0] = new float[] { .16f, .26f, .16f, .13f };
        //m_Blobs[1] = new float[] { .13f, -.134f, .35f, .12f };
        //m_Blobs[2] = new float[] { -.18f, .125f, -.25f, .16f };
        //m_Blobs[3] = new float[] { -.13f, .23f, .255f, .13f };
        //m_Blobs[4] = new float[] { -.18f, .125f, .35f, .12f };

        m_MetaBalls = new MetaBalls(xCubeCount, yCubeCount, zCubeCount)
        {
            blobs = m_Blobs 
        };

    }

    // Update is called once per frame
    void Update()
    {
        //m_Blobs[0][0] = .12f + .12f * (float)Mathf.Sin((float)Time.time * .50f);
        //m_Blobs[0][2] = .06f + .23f * (float)Mathf.Cos((float)Time.time * .2f);
        //m_Blobs[1][0] = .12f + .12f * (float)Mathf.Sin((float)Time.time * .2f);
        //m_Blobs[1][2] = -.23f + .10f * (float)Mathf.Cos((float)Time.time * 1f);
        //m_Blobs[2][1] = -.03f + .24f * (float)Mathf.Sin((float)Time.time * .35f);
        //m_Blobs[3][1] = .126f + .10f * (float)Mathf.Cos((float)Time.time * .1f);
        //m_Blobs[4][0] = .206f + .1f * (float)Mathf.Cos((float)Time.time * .5f);
        //m_Blobs[4][1] = .056f + .2f * (float)Mathf.Sin((float)Time.time * .3f);
        //m_Blobs[4][2] = .25f + .08f * (float)Mathf.Cos((float)Time.time * .2f);
        //transform.Rotate(Time.deltaTime * 10f, 0, Time.deltaTime * .6f);

        //m_MetaBalls.March();
    }

    private Bounds GetCubeBound(McCube cube)
    {
        Vector3 _min = Vector3.one * float.MaxValue;
        Vector3 _max = Vector3.one * float.MinValue;

        /*for(int i=0; i<cube.posIndex.Length; i++)
        {
            var position = m_MetaBalls.points[cube.posIndex[i]].position;
            _min.x = Mathf.Min(_min.x, position.x);
            _min.y = Mathf.Min(_min.y, position.y);
            _min.z = Mathf.Min(_min.z, position.z);

            _max.x = Mathf.Max(_max.x, position.x);
            _max.y = Mathf.Max(_max.y, position.y);
            _max.z = Mathf.Max(_max.z, position.z);
        }*/

        return new Bounds{ min = _min, max = _max };
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(transform.localPosition,Vector3.one);
        if (m_MetaBalls == null)
            return;

        /*for(int i=0; i<m_MetaBalls.points.Length; i++)
        {
            var point = m_MetaBalls.points[i];
            if (point.ios > 1.0f)
                Gizmos.color = Color.red;
            else
                Gizmos.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            Gizmos.DrawSphere(point.position, 0.01f);
        }

        for(int i=0; i<m_MetaBalls.cubes.Length; i++)
        {
            if(m_MetaBalls.cubes[i].index > 0)
                Gizmos.color = new Color(0f, 1f,0f, 1f);
            Bounds b = GetCubeBound(m_MetaBalls.cubes[i]);
            Gizmos.DrawCube(b.center, b.size);
        }

        Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
        for (int i=0; i<m_Blobs.Length; i++)
        {
            Gizmos.DrawSphere(new Vector3(m_Blobs[i][0],m_Blobs[i][1],m_Blobs[i][2]), m_Blobs[i][3]);
        }*/
    }
}
