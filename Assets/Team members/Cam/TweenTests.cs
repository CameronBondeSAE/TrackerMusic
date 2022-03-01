using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenTests : MonoBehaviour
{
	public float movingValue;
	
    //
    // // Update is called once per frame
    // void Update()
    // {
	   //  // Use variables
	   //  movingValue = Mathf.MoveTowards(movingValue, 1f, 0.05f);
    //
	   //  transform.position = new Vector3(movingValue,0,0);
    // }
    //
    public AnimationCurve Curve1;
    private float timer;

    public Transform thingToMessWith;

    public void Update()
    {
	    timer += Time.deltaTime;
	    // Read values from the AnimationCurve in the inspector
	    float animatedValue = Curve1.Evaluate(timer);


// Simple scale for example
	    thingToMessWith.localScale = new Vector3(animatedValue, animatedValue, animatedValue);

	    if (timer >= Curve1.length)
	    {
// Loop
		    timer = 0;
	    }
    }
}
