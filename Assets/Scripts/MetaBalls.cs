using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RIU.MarchingCubes
{
    public class MetaBalls : MarchingCubes
    {
        public MetaBalls(int cubeNumX, int cubeNumY, int cubeNumZ)
            : base(cubeNumX, cubeNumY, cubeNumZ)
        {
        }

        protected override void AddEdge(ref McEdge edge)
        {
            throw new System.NotImplementedException();
        }

        protected override void AddTriangles(int vi1, int vi2, int vi3)
        {
            throw new System.NotImplementedException();
        }

        protected override float CalculateIos(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
    }
}
