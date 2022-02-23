using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class VoxelActions : MonoBehaviour
{
	public Vector3 initialCondition;
	public float threshold;
	public float slowness = 10f;
	public float noiseValue;
	
    // Start is called before the first frame update
    void Start()
    {
	    
    }

    // Update is called once per frame
    void Update()
    {
	    //Update noiseValue.
	    noiseValue = (float) NoiseS3D.Noise(initialCondition[0] + Time.time / slowness,
		    initialCondition[1] + Time.time / slowness, initialCondition[2] + Time.time / slowness);
	    
	    //Scale shape.
	    transform.localScale = new Vector3(Mathf.PerlinNoise(Time.time * 3f, 0)+4.5f,
		    Mathf.PerlinNoise(Time.time + 435f, 0)+1.5f, Mathf.PerlinNoise(Time.time*2f, 0)+3.5f);
	    
	    //Set visibility.
	    GetComponent<Renderer>().enabled = noiseValue > threshold;
	    if (GetComponent<Renderer>().enabled)
	    {
		    //Change Colour.
		    //GetComponent<Renderer>().material.color = new Color(Mathf.PerlinNoise(Time.time * 1.5f, 0)*0.5f, Mathf.PerlinNoise(Time.time + 435f, 0)*0.5f, Mathf.PerlinNoise(Time.time, 0)*0.5f, Mathf.Clamp(noiseValue - 0.9f, 0f, 1f));
		    GetComponent<Renderer>().material.color = new Color(0.1f, 0.1f, 0.1f, Mathf.Clamp(noiseValue - 0.9f, 0f, 1f));
	    }
    }
}
