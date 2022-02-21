using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public GameObject sphereObject;
    //public GameObject tailObject;
    public int snakeLength;
    // Start is called before the first frame update
    void Start()
    {
        GameObject headObject = Instantiate(sphereObject); 

        for (int i = 0; i < snakeLength; i++)
        {
            GameObject tailObject = Instantiate(sphereObject);
            tailObject.transform.parent = headObject.transform; 
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
