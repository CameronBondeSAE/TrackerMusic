using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Aaron
{
    public class AaronShaderScript : MonoBehaviour
    {
        public MeshRenderer meshRenderer;

        public float time;

        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            
            FindObjectOfType<AaronMusic>().InstrumentPlayEvent += ChangeColour;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ChangeColour()
        {
            meshRenderer.material.SetColor("_Colour", new Color(Random.value, Random.value, Random.value, 1f));
        }
    }
}