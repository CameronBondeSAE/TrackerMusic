using System.Collections;
using System.Collections.Generic;
using SharpMik;
using SharpMik.Player;
using DG.Tweening;
using UnityEngine;

public class JohnShaderTest : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;

    public MeshRenderer meshRenderer;
    public GameObject sphere;

    public float maxSize = 3f;
    public float minSize = 1f;
    public float duration = 3f;

    [Header("Instruments")]
    public int bassline = 5;
    public int lazerNoises = 17;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

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
        meshRenderer.material.SetColor("_Color", new Color(0.2f, newNotePlayed.main.sample/newNotePlayed.volume, newNotePlayed.anote/3));

        //Specific Instruments
        if(newNotePlayed.main.sample == bassline)
        {
            DOTween.To(JustASetter, maxSize, minSize, duration);
        }

        if (newNotePlayed.main.sample == lazerNoises)
        {
            GameObject go = Instantiate(sphere);
            go.transform.position = new Vector3(newNotePlayed.anote, 0, 0) + new Vector3(newNotePlayed.main.sample, 0, 0);
            Destroy(go, 3f);
        }

    }

    void JustASetter(float newValue)
    {
        transform.localScale = new Vector3(newValue, newValue, newValue);
    }

}
