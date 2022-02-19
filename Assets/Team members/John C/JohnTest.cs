using System.Collections;
using System.Collections.Generic;
using SharpMik;
using SharpMik.Player;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class JohnTest : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public GameObject cubeObject;
    public Color newColour;


    void Start()
    {
        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
    }

    // GPG230 stuff
    private void ModPlayerOnNoteEvent(MP_CONTROL mpControl)
    {
        UnityThread.executeInUpdate(() =>
        {
            NotePlayedEvent(mpControl);
        });
    }

    private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    {
        GameObject go = Instantiate(cubeObject,Vector3.zero, Quaternion.identity);
        go.GetComponent<CubeLogic>().note = newNotePlayed.anote;
        go.GetComponent<Renderer>().material.color = newColour;


    }

    void Update()
    {
        // pick a random color.
        newColour = new Color( Random.value, Random.value, Random.value, 1.0f );
    }
    
}
