using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class AaronShaderMuckAround : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public MeshRenderer meshRenderer;
    public Transform transformObject;

    public int instrumentIndex;
    public int noteIndex;
    public int scaleMultiplier;

    public Vector3 scaleToGrowTo;
    public float growthTimer;

    private Vector3 currentScale;
    private Vector3 newScale;

    void Start()
    {
        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        //Is there a better way to grab this? Besides back to back GetComponents?
        meshRenderer = GetComponent<AaronShaderMuckAround>().GetComponent<MeshRenderer>();

        currentScale = meshRenderer.material.GetVector("_Scale");
        //StartCoroutine(LerpScale(scaleToGrowTo, growthTimer));

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

    //Scale in bit
    /*IEnumerator LerpScale(Vector3 targetScale, float duration)
    {
        float time = 0;
        
        Vector3 startScale = transformObject.transform.localScale;

        while (time < duration)
        {
            transformObject.transform.localScale = Vector3.Lerp(startScale, targetScale, time/duration);
            time += Time.deltaTime;
            yield return null;
        }

        transformObject.transform.localScale = targetScale;
    }*/
}