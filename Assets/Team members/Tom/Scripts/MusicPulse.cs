using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

namespace Tom
{
    public class MusicPulse : MonoBehaviour
    {
        public SharpMikManager sharpMikManager;
        public int instrument;
        public float scaleMultiplier = 0.1f;
    
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
            if (newNotePlayed.main.sample == instrument)
            {
                DOTween.To(GetScale, SetScale, newNotePlayed.volume * scaleMultiplier, 1f).OnComplete(ResetScale);
            }
        }

        private void SetScale(float newValue)
        {
            transform.localScale = new Vector3(newValue, newValue, newValue);
        }

        private float GetScale()
        {
            return transform.localScale.x;
        }

        private void ResetScale()
        {
            DOTween.To(GetScale, SetScale, 1f, 1f);
        }
    }
}