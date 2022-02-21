using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik.Player;
using UnityEngine;

public class KevinCube : MonoBehaviour
{
    public byte instrument;
    public byte note;
    

    public short volume;
    public Material changeMaterial;
    

    public void ScaleSphere(float newValue)
    {
        transform.localScale = new Vector3(newValue, newValue, newValue);
    }
    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(note, 0f, 0f);
        //DOTween.To(ScaleSphere, 1, volume / 20f, 0.5f);
        DOTween.To(SphereColor, 1f, 5f / 10f, 1f);
        //DOTween.To(MoveSphere, note, 20f, 1f);
        //DOTween.To(MoveSphere, -note, 20f, 1f);
	    Destroy(gameObject,5f);
    }

    void SphereColor(float pnewvalue)
    {
        changeMaterial = GetComponent<Renderer>().material;
        changeMaterial.color = new Color(Random.Range(0,4) , Random.Range(0,4)  ,Random.Range(0,4) );
    }

    public void MoveSphere(float newValue)
    {
        transform.position = new Vector3(newValue, newValue, newValue);
    }
    
}
