using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik.Player;
using UnityEngine;

public class Centrepiece : MonoBehaviour
{
    public Material changeMaterial; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision balls)
    {
        changeMaterial = GetComponent<Renderer>().material;
        changeMaterial.color = new Color(Random.Range(0,4) , Random.Range(0,4)  ,Random.Range(0,4) );
    }
}
