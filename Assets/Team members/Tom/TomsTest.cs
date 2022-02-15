using System.Collections;
using System.Collections.Generic;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class TomsTest : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public GameObject cubePrefab;
    public float forceMultiplier = 1f;
    
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
        Vector3 spawnPosition = new Vector3(newNotePlayed.main.sample, 0, 0);
        GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        newCube.GetComponent<Rigidbody>().AddForce(Vector3.up * newNotePlayed.volume * forceMultiplier);
        Destroy(newCube, 5f);
    }
}
