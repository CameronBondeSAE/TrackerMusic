using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneEffects : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Material planeMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        planeMaterial = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
