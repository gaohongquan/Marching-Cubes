using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RIU.MarchingCubes
{
    public class MetaBalls : MarchingCubes
    {
        // 0=x, 1=y 2=z 3=power
        private float[][] m_Blobs;

        public MetaBalls(int cubeNumX, int cubeNumY, int cubeNumZ)
            :base(cubeNumX,cubeNumY,cubeNumZ)
        {
            isoLevel = 1.0f;
        }

        public float isoLevel { get; set; }

        public float[][] blobs { set { m_Blobs = value; } }

        protected override float OnCalculateIossurface(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        //protected override bool OnCheckIossurface(float ios)
        //{
        //    throw new System.NotImplementedException();
        //}

        //protected override Vector3 OnInterpolation(McPoint p1, McPoint p2)
        //{
        //    throw new System.NotImplementedException();
        //}

        /*protected override void OnCalculateIossurface()
        {
            for(int i=0; i< m_Points.Length; i++)
            {
                m_Points[i].ios = 0;
                for (int j=0; j<m_Blobs.Length; j++)
                {
                    float[] blob = m_Blobs[j];
                    m_Points[i].ios += (1.0f / Mathf.Sqrt(
                        ((blob[0] - m_Points[i].position.x) * (blob[0] - m_Points[i].position.x))+
                        ((blob[1] - m_Points[i].position.y) * (blob[1] - m_Points[i].position.y))+
                        ((blob[2] - m_Points[i].position.z) * (blob[2] - m_Points[i].position.z)))
                        )*blob[3];
                }
            }
        }*/
    }
}
