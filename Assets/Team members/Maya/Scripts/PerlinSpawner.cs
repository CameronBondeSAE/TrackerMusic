using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PerlinSpawner : MonoBehaviour
{
    public GameObject origin;
    public int maxCubes;
    public Vector3 originOffset;
    public Vector3 sceneFloor;
    public GameObject prefabCube;
    public float divisor;


    // Start is called before the first frame update
    void Start()
    {
        sceneFloor = new Vector3(0, -5, -15);
        originOffset = new Vector3(maxCubes / 2f, 0, maxCubes / 2f);
        //MakeSomeNoise();
    }

    public void MakeSomeNoise()
    {
        //sceneFloor = new Vector3(0, -5, -15);
        
        origin = new GameObject("middle");
        origin.transform.position = sceneFloor;
        //origin = Instantiate(origin, sceneFloor, Quaternion.identity);
        for (int j = 0; j < maxCubes; j++)
        {
            for (int i = 0; i < maxCubes; i++)
            {
                GameObject spawnedCube = Instantiate(prefabCube, origin.transform);
                spawnedCube.transform.localPosition = new Vector3(j - originOffset.x, Mathf.PerlinNoise(j/divisor, i/divisor), i - originOffset.z);
                spawnedCube.transform.localScale = new Vector3(1, Mathf.PerlinNoise(j/divisor, i/divisor)*2f, 1);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
