using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public GameObject sphereObject;
    //public GameObject tailObject;
    public int snakeLength;
    public Vector3 offset;
    public List<Transform> sphereTransforms;
    // Start is called before the first frame update
    void Start()
    {
        Transform previousTransform = transform; 
        
        for (int i = 0; i < snakeLength; i++)
        {
            GameObject snakeObject = Instantiate(sphereObject);
            sphereTransforms.Add(snakeObject.transform);

            if (i > 0)
            {
                snakeObject.transform.parent = previousTransform;
                snakeObject.transform.localPosition = offset;
            }

            previousTransform = snakeObject.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform sphereTransform in sphereTransforms)
        {
            sphereTransform.transform.DORotate(new Vector3(Random.Range(0f,20f),Random.Range(0f,20f),Random.Range(0f,20f)),5f);
        }
        
    }
}
