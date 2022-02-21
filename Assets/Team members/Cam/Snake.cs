using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	public int number = 100;

	public GameObject prefab;

	public Vector3 offset;

	public List<Transform> cubeTransforms;
	public Vector3 twistAmount;
	// Start is called before the first frame update
    void Start()
    {
	    Transform lastTransform = transform;

	    for (int i = 0; i < number; i++)
	    {
		    GameObject o = Instantiate(prefab);
		    cubeTransforms.Add(o.transform);
		    if (i > 0)
		    {
			    o.transform.parent = lastTransform;
			    o.transform.localPosition = offset;
		    }

		    lastTransform = o.transform;
	    }
    }

    // Update is called once per frame
    void Update()
    {
	    foreach (Transform cubeTransform in cubeTransforms)
	    {
		    cubeTransform.Rotate(twistAmount*Time.deltaTime, Space.Self);
	    }

    }
}
