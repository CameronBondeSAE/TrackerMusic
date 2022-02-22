using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik.Player;
using UnityEngine;

public class Centrepiece : MonoBehaviour
{
    public Material changeMaterial;

    public short volume;
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
        DOTween.To(ScaleSphere, 10, volume, 0.5f);
    }
    
    public void ScaleSphere(float newValue)
    {
        transform.localScale = new Vector3(newValue, newValue, newValue);
    }
}
