using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SharpMik;
using SharpMik.Player;
using UnityEditor.Search;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ollie : MonoBehaviour
{
    // The music player
    public SharpMikManager sharpMikManager;
    public GameObject prefabNote;
    private GameObject cube;
    public List<GameObject> cubes;
    private Vector3 targetPosition;
    private Vector3 currentPosition;
    private float duration;
    public short volume;
    
    public Vector3 deadCubePosition;
    public GameObject spherePrefab;
    
    
    void Start()
    {
        // Subscribing to C# Event when a note plays
        ModPlayer.NoteEvent += ModPlayerOnNoteEvent;

        // GPG230 stuff
        UnityThread.initUnityThread();
        
        cubes = new List<GameObject>();
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
        targetPosition = new Vector3(0,0,0);

        if (newNotePlayed.main.sample >= 10)
        {
            cube = Instantiate(prefabNote);
            cube.GetComponent<OllieCube>().ollie = this;
            cube.transform.position = new Vector3(Random.Range(-70, 70), Random.Range(-20, 10), 350);
            cubes.Add(cube);
        }

        if (newNotePlayed.main.sample == 3 && cubes.Count>= 1)
        {
            GameObject oneCube = cubes[Random.Range(0, cubes.Count)];
            deadCubePosition = oneCube.transform.position;
            GameObject o = Instantiate(spherePrefab);
            o.transform.position = deadCubePosition;
            cube.GetComponent<OllieCube>().tweener.Kill();
            Destroy(oneCube);
        }

        //set volume publicly to access from sphere?
        volume = newNotePlayed.volume;
    }
}