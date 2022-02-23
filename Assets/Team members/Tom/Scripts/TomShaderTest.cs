using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tom
{
    public class TomShaderTest : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        private Color nextColour;
        public float colourChangeRate = 0.1f;

        private void Start()
        {
            nextColour = new Color(Random.value,Random.value,Random.value);
        }

        void Update()
        {
            if (GetMeshColour() == nextColour)
            {
                nextColour = new Color(Random.value,Random.value,Random.value);
            }
        
            meshRenderer.material.SetColor("_Colour", Color.Lerp(GetMeshColour(), nextColour, colourChangeRate * Time.deltaTime));
        }

        private Color GetMeshColour()
        {
            return meshRenderer.material.GetColor("_Colour");
        }
    }
}