using System;
using System.Collections;
using System.Collections.Generic;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

namespace Aaron
{
    public class AaronMusic : MonoBehaviour
    {
        // The music player
        public SharpMikManager sharpMikManager;

        public Transform cubePrefab;

        public int instrumentIndex;

        public event Action InstrumentPlayEvent;

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
            UnityThread.executeInUpdate(() => { NotePlayedEvent(mpcontrol); });
        }

        private void NotePlayedEvent(MP_CONTROL newNotePlayed)
        {
            // Your code goes here
            if (newNotePlayed.main.sample == instrumentIndex)
            {
                Instantiate(cubePrefab, new Vector3(0, 0, 0), cubePrefab.transform.rotation);
                InstrumentPlayEvent?.Invoke();
            }
        }
    }
}