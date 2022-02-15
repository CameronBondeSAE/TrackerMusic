using System.Collections;
using System.Collections.Generic;
using SharpMik;
using SharpMik.Player;
using UnityEngine;

public class Ollie : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public GameObject prefabNote;
    private GameObject cube;
    private List<GameObject> cubes;
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private float speed = 0.5f;
    
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

    // Your code goes here
    private void NotePlayedEvent(MP_CONTROL newNotePlayed)
    {
        cubes = new List<GameObject>();
        targetPosition = new Vector3(0,0,0);

        if (newNotePlayed.main.sample >= 10)
        {
            cube = Instantiate(prefabNote);
            cube.transform.position = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20));
            cubes.Add(cube);
        }
        
        for (int i = 0; i < cubes.Count; i++)
        {
            // currentPosition = new Vector3(10,10,0);
            // print("position = " + currentPosition);
            // cube.transform.position = Vector3.Lerp(currentPosition,targetPosition, 2f);
            // print("target = " + targetPosition);
            if (cube.transform.position != targetPosition)
            {
                Vector3 newPos = Vector3.MoveTowards(cube.transform.position, targetPosition, speed * Time.deltaTime);
                cube.transform.position = newPos;
            }
        }
    }
}