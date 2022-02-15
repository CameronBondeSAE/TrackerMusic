using System;
using System.Collections;
using System.Collections.Generic;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class LukeTest : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public GameObject prefabNote;
    
    void Start()
    {
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
        GameObject go = Instantiate(prefabNote);
        go.transform.position = new Vector3(newNotePlayed.anote/10, newNotePlayed.volume/100, 0);

        if (newNotePlayed.main.sample == 0)
        {
	        go.transform.position = go.transform.position + new Vector3(MathF.Cos(Time.time)-10,0,MathF.Sin(Time.time));
        }
        
        
        if (newNotePlayed.volume < 50)
        {
	        go.transform.position = new Vector3 (5, 0, 0);
        }
    }
}
