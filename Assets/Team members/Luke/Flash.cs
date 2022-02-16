using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Flash : MonoBehaviour
{
	public byte instrument;
	public byte note;
	public short volume;
	public int intensity;
	public float duration;
	private int onDuration;
	private int offDuration;

	void SetIntensity(float newValue)
	{
		GetComponent<Light>().intensity = newValue;
	}

	private IEnumerator TurnOnTurnOff()
	{
		DOTween.To(SetIntensity, 0f, intensity, onDuration);
		yield return new WaitForSeconds(onDuration);
		DOTween.To(SetIntensity, intensity, 0f, offDuration);
	}

	void Awake()
	{
		onDuration = Mathf.CeilToInt(duration * 2 / 3);
		offDuration = Mathf.FloorToInt(duration / 3);
	}
	
	
	// Start is called before the first frame update
    void Start()
    {
	    transform.position = new Vector3(note+Random.Range(-5f,5f), 0f, -5f);
	    StartCoroutine(TurnOnTurnOff());
	    Destroy(gameObject, duration+1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
