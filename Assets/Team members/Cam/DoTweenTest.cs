using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class DoTweenTest : MonoBehaviour
{
	
	// The music player
	public SharpMikManager sharpMikManager;

	public Quaternion startRotation;
	
	void Start()
	{
		// Subscribing to C# Event when a note plays
		ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

		// GPG230 stuff
		UnityThread.initUnityThread();

		// Cheat version using convenience functions made by DoTween guy
	    // transform.DOScale(new Vector3(5f, 5f, 5f), 4f);


	    
    }

	// GPG230 stuff
	private void ModPlayerOnNoteEvent(MP_CONTROL mpcontrol)
	{
		UnityThread.executeInUpdate(() =>
		{
			NotePlayedEvent(mpcontrol);
		});
	}
	
	private void NotePlayedEvent(MP_CONTROL mpcontrol)
	{
		// Every time instrument 0 gets triggered
		Debug.Log(mpcontrol.main.sample);
		
		if (mpcontrol.main.sample == 4)
		{
			startRotation = Random.rotation;
			
			DOTween.To(CamSetter, 0, 1f, 0.5f);
		}
	}

	private void CamSetter(float pnewvalue)
    {
	    // transform.rotation = Quaternion.Lerp(transform.rotation, );
    }

    // Polling is bad for some things
    void Update()
    {
        
    }
}
