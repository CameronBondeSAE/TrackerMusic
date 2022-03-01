using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelSpawner : MonoBehaviour
{
	public GameObject voxelPrefab;
	public int voxelsInX;
	public int voxelsInY;
	public int voxelsInZ;
	public float waveLength = 10f;
	public float perlinThreshold = 0.3f;
	
	// Start is called before the first frame update
    void Start()
    {
	    for (var i=0; i<voxelsInX; i++)
	    {
		    for (var j=0; j<voxelsInY; j++)
		    {
			    for (var k=0; k<voxelsInZ; k++)
			    {
				    float noiseValue = (float) (NoiseS3D.Noise(i/waveLength, j/waveLength, k/waveLength)+1)/2f;
				    
				    GameObject go = Instantiate(voxelPrefab, gameObject.transform);
				    go.name = i.ToString() + "." + j.ToString() + "." + k.ToString();
				    go.transform.position = new Vector3(i,1,k);
				    go.GetComponent<VoxelActions>().initialCondition = go.transform.position/waveLength;
				    go.GetComponent<VoxelActions>().threshold = perlinThreshold;
			    }
		    }
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
