using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class KevinTest : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public GameObject prefabCube;
    public Vector3 offset;
    public float speed = 5f;
    
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

    private void NotePlayedEvent(MP_CONTROL mpControl)
    {
        // Your code goes here
        GameObject cubeObject = Instantiate(prefabCube);short mpControlVolume = (short) (mpControl.volume / 40);
        cubeObject.transform.position = new Vector3(mpControl.anote, 0, 0) + new Vector3(mpControl.main.sample, 0, 0);
        cubeObject.transform.localScale = new Vector3(Random.Range(1,5), Random.Range(1,5),Random.Range(1,5));
        cubeObject.GetComponent<MeshRenderer>().material.color = Color.green;
    }
    
    
}
