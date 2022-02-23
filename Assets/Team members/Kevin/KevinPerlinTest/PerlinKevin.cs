using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinKevin : MonoBehaviour
{
    public GameObject tO;

    public int terrainLength;
    public float xCoord;
    public float yCoord; 
    //public float amplitude = 10f; 
    //public float waveLength = 10f; 
    
    // Start is called before the first frame update
    void Start()
    {
        for (int t = 0; t < terrainLength; t++)
        {
            for (int i = 0; i < terrainLength; i++)
            {
                Instantiate(tO);
                tO.transform.position = new Vector3(t-terrainLength, 1 + 2f * Mathf.PerlinNoise(t/10f, i/10f), i-terrainLength);
                tO.transform.localScale = new Vector3(1, 1 + 2f * Mathf.PerlinNoise(t/10f, i/10f)*2f, 1);
                //tO.transform.position = new Vector3(0f, 1 + 10f * Mathf.PerlinNoise(t / 10f, i / 10f), 1);
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector3(Mathf.PerlinNoise(Time.time, 0f), Mathf.PerlinNoise(Time.time, 20f), Mathf.PerlinNoise(Time.time, 15f));
    }
}
