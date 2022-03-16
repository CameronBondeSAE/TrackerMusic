using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rain : MonoBehaviour
{
	public byte instrument;
	public byte note;
	public short volume;
	public Color colour;
	public float minXSpawn = 0f;
	public float maxXSpawn = 10f;
	public float minZSpawn = 0f;
	public float maxZSpawn = 10f;
	
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
	void OnEnable()
	{
		GetComponent<ParticleSystemRenderer>().material.color = colour;
		transform.position = new Vector3(Random.Range(minXSpawn,maxXSpawn), 0f, minZSpawn+instrument/32f*(maxZSpawn-minZSpawn));
		//StartCoroutine(TurnOnTurnOff());
		Destroy(gameObject,5f);
	}
    
    
}