using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PerlinTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale =
            new Vector3(Mathf.PerlinNoise(Time.time, 0f), Mathf.PerlinNoise(Time.time, 20f), Mathf.PerlinNoise(Time.time, 15f));
    }
}
