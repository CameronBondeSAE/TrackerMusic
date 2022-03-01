using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Aaron
{
    public class AaronShaderScript : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public int changeTimer = 1;

        private Color color;
        
        // Start is called before the first frame update
        void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            FindObjectOfType<MusicThing>().InstrumentPlayedEvent += ChangeColour;
        }

        void ChangeColour()
        {
            meshRenderer.material.SetColor("_Colour",Colour());
        }

        Color Colour()
        {
            return color = new Color(Random.value, Random.value, Random.value, 1f);
        }
    }
}