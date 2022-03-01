using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
	public byte instrument;
	public byte note;
	public short volume;
	public Color colour;
	
	private IEnumerator TurnOnTurnOff()
	{
		DOTween.To(ScaleCylinder, 1f, (1+instrument)/3f+volume/20f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		DOTween.To(ScaleCylinder, (1+instrument)/3f+volume/20f, 1f, 0.5f);
	}
	
	public void ScaleCylinder(float newValue)
	{
		transform.localScale = new Vector3(newValue,1,newValue);
	}

	// Start is called before the first frame update
    void Start()
    {
	    GetComponent<Renderer>().material.color = colour;
	    transform.position = new Vector3(note, 0f, 1+instrument);
	    StartCoroutine(TurnOnTurnOff());
	    Destroy(gameObject,2f);
    }
    
    
}