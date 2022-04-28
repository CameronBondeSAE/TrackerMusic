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

    public event Action InstrumentPlayedEvent;

    public event Action<int> Instrument15PlayedEvent;
    public event Action<int> Instrument20PlayedEvent;
    public event Action<int> Instrument26PlayedEvent;
    public event Action<int> NewInstrumentEvent;

    public int instrumentIndex;
    public int noteIndex;

    
    void Start()
    {
        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
    }
    
    private void Update()
    {
        MP_CONTROL mpcontrol = new MP_CONTROL();
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


        /*if (newNotePlayed.main.sample == 15)
        {
            Instrument15PlayedEvent?.Invoke(newNotePlayed.main.sample);
        }
        
        //Later in the song; goes mental though
        if (newNotePlayed.main.sample == 20)
        {
            Instrument20PlayedEvent?.Invoke(newNotePlayed.main.sample);
        }
     
        if (newNotePlayed.main.sample == 26)
        {
            Instrument26PlayedEvent?.Invoke(newNotePlayed.main.sample);
            //Debug.Log(newNotePlayed.main.note);
        }

        if (newNotePlayed.main.sample == instrumentIndex)
        {
            NewInstrumentEvent?.Invoke(newNotePlayed.main.sample);
        }*/
    
}            

