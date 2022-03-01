using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalWorldTest : MonoBehaviour
{
	public float speed;
	public float positionValue;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.Translate(0,0,speed * Time.deltaTime, Space.Self);

        transform.localPosition = new Vector3(0,0,positionValue);
    }
}
