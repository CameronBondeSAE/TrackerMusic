using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
	public GameObject prefab;
    //
    //
    
    // Start is called before the first frame update
    void Start()
    {
	    GameObject go1 = Instantiate(prefab, gameObject.transform);
	    Debug.Log(go1.transform.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
