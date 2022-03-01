using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class MusicThing : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;

    public int instrumentIndex;

    public event Action InstrumentPlayedEvent;
    
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
        if (newNotePlayed.main.sample == instrumentIndex)
        {
            
            
            InstrumentPlayedEvent?.Invoke();
        }
        
    }
}