using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class AaronShaderMuckAround : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public MeshRenderer meshRenderer;

    public int instrumentIndex;
    public int noteIndex;
    public int scaleMultiplier;

    private Vector3 currentScale;
    private Vector3 newScale;

    void Start()
    {
        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        //Is there a better way to grab this? Besides back to back GetComponents?
        meshRenderer = GetComponent<AaronShaderMuckAround>().GetComponent<MeshRenderer>();

        currentScale = meshRenderer.material.GetVector("_Scale");

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
            meshRenderer.material.SetVector("_Scale", new Vector3(newNotePlayed.anote * scaleMultiplier, 0,0));
        }
    }
}