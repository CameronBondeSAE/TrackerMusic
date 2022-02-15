using System.Collections;
using System.Collections.Generic;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class MayaStuff : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    
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
        // Debug.Log(newNotePlayed.anote + " : Vol = "+newNotePlayed.volume);
        
    }
}
