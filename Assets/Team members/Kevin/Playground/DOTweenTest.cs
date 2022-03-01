using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DOTweenTest : MonoBehaviour
{
    public float movingValue;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        movingValue = Mathf.Lerp(movingValue, 5f, 0.1f);
        transform.position = new Vector3(movingValue, 0, 0);
        //transform.position = Vector3.Lerp(transform.position, Vector3.one, 0.05f);
    }

   
}
