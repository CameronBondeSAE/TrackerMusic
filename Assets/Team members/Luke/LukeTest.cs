using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class LukeTest : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public GameObject prefabRain;
    public GameObject prefabFlash;
    public List<Color> colours = new List<Color>(32);
    public List<byte> instrumentList;
    public int instrumentIndex;

    void Start()
    {
	    for (int i = 0; i < 32; i++)
	    {
		    colours.Add(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
	    }
	    
	    // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
    }

    

    // GPG230 stuff
    private void ModPlayerOnNoteEvent(MP_CONTROL mpcontrol)
    {
        UnityThread.executeInUpdate(() =>
        {
            NotePlayedEvent(mpcontrol);
        });
    }

    private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    { 
	    // Your code goes here
	    byte instrument = newNotePlayed.main.sample;
	    byte note = newNotePlayed.anote;
	    short volume = newNotePlayed.volume;

	    if (!instrumentList.Contains(instrument))
	    {
		    instrumentList.Add(instrument);
	    }
	    instrumentIndex = instrumentList.IndexOf(instrument);

	    switch (instrumentIndex)
	    {
		    case 0:
			    //Cloud
			    //code
			    break;
		    case 1:
			    //Rain
			    GameObject go = Instantiate(prefabRain);
			    Cylinder goScript = go.GetComponent<Cylinder>();
			    goScript.instrument = instrument;
			    goScript.note = note;
			    goScript.volume = volume;
			    goScript.colour = colours[instrument];
			    break;
		    case 2:
			    //Lightning
			    GameObject flash = Instantiate(prefabFlash);
			    Flash flashScript = flash.GetComponent<Flash>();
			    flashScript.instrument = instrument;
			    flashScript.note = note;
			    flashScript.volume = volume;
			    flashScript.intensity = volume*10;
			    flashScript.duration = volume/10;
			    break;
		    default:
			    goto case 2;
	    }
    }
}
