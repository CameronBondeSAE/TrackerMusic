using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    public float movingValue;
    public AnimationCurve Curve1;
    private float timer;
    public int note;
    public Transform thingToMessWith;
    public float speedOfCubeMovement;
    //public float sizeOfCubes;
    public Renderer cubeColours;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void Update()
    {
       
        movingValue = Mathf.Lerp(movingValue, 10f, speedOfCubeMovement*Time.deltaTime);
        timer += Time.deltaTime;
        // Read values from the AnimationCurve in the inspector
        float animatedValue = Curve1.Evaluate(timer);
        this.transform.Translate(movingValue, 0, 0);


// Simple scale for example
        thingToMessWith.localScale = new Vector3(animatedValue, animatedValue, animatedValue);
        transform.Rotate(0,note*Time.deltaTime,0);
        //transform.translate in self mode // moving in local space relative to a parent 
        //transform.parent // to parent the cubes to the planet
        
    }

    void ChangeColour()
    {
        // pick a random color.
        Color newColor = new Color( Random. value, Random. value, Random. value, 1.0f );
        // apply it on current object's material.
        cubeColours.material.color = newColor;
    }
}
