using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowOnBeat : MonoBehaviour
{
    public Transform target;

    private float currentSize;
    [Header("Behaviour Settings")] 
    public float growSize;
    public float shrinkSize;
    [Range(0.8f, 0.99f)]
    public float shrinkFactor;

    [Header("Beat Settings")] 
    [Range(0, 3)]
    public int onFullbeat;
    [Range(0,7)]
    public int[] onBeatD8;
    private int beatCountFull;
    // Start is called before the first frame update
    void Start()
    {
        //if there's no target set in Inspector
        //target becomes the transform this script is on
        if (target == null)
        {
            target = this.transform;
        }

        currentSize = shrinkSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSize > shrinkSize)
        {
            currentSize *= shrinkFactor;
        }
        else
        {
            currentSize = shrinkSize;
        }
        CheckInstrument();
        target.localScale = new Vector3(target.localScale.x, currentSize, target.localScale.z);
    }

    void Grow()
    {
        currentSize = growSize;
    }

    void CheckInstrument()
    {
        
    }
}
