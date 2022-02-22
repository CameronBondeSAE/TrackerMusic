using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTest : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float scale = 5f;

    private void Update()
    {
        meshRenderer.material.SetFloat("_Multiplier", Mathf.PerlinNoise(Time.time, 0) * scale);
    }
}
