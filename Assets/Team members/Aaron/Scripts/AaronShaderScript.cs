using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aaron
{
    public class AaronShaderScript : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        
        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            meshRenderer.material.SetColor("_Colour", new Color(Random.value, Random.value, Random.value, 1f));
        }
    }
}