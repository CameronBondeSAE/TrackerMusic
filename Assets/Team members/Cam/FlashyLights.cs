using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashyLights : MonoBehaviour
{
	public Light light;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
	    light.intensity = Random.Range(0, 100000);
    }
}
